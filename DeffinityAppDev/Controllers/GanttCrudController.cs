﻿using Bryntum.CRUD.Response;
using Bryntum.Gantt;
using Bryntum.Gantt.Request;
using Bryntum.Gantt.Request.Handler;
using Bryntum.Gantt.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeffinityAppDev.Controllers
{
    /// <summary>
    /// Summary description for GanttCrudController
    /// </summary>
    public partial class GanttCrudController : Controller
    {
        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            base.Execute(requestContext);
        }
        public ActionResult Index()
        {
            var r = new ContentResult();
            r.Content = "Hello World";
            return r;
        }

        public const string dateFormat = "yyyy-MM-dd\\THH:mm:ss";
        //public const string dateFormat = "dd/MM/yyyy\\THH:mm:ss";

        /// <summary>
        /// Helper method to get POST request body.
        /// </summary>
        /// <returns>POST request body.</returns>
        private string getPostBody()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            return new StreamReader(req).ReadToEnd();
        }

        /// <summary>
        /// Load request handler.
        /// </summary>
        /// <returns>JSON encoded response.</returns>
        [System.Web.Mvc.HttpGet]
        public ActionResult Load(int id)
        {
            var uid = sessionKeys.UID;
            var projectref = id;
            GanttLoadRequest loadRequest = null;
            ulong? requestId = null;

            try
            {
                string json = Request.QueryString.Get("q");

                var gantt = new Gantt();

                // decode request object
                try
                {
                    loadRequest = JsonConvert.DeserializeObject<GanttLoadRequest>(json);
                }
                catch (Exception)
                {
                    throw new Exception("Invalid load JSON");
                }

                // get request identifier
                requestId = loadRequest.requestId;

                // initialize response object
                var loadResponse = new GanttLoadResponse(requestId);

                // if a corresponding store is requested then add it to the response object

                if (loadRequest.calendars != null) loadResponse.setCalendars(gantt.getCalendars(), 1);
                //LogExceptions.LogException(gantt.getCalendars().ToList().Count.ToString(), "Calender");
                if (gantt.getAssignmentsCount(Convert.ToInt32(projectref)) > 0)
                {
                    if (loadRequest.assignments != null) loadResponse.setAssignments(gantt.getAssignments(Convert.ToInt32(projectref)));
                }
                if (loadRequest.tasks != null) loadResponse.setTasks(gantt.getTasks(Convert.ToInt32(projectref)));
                //LogExceptions.LogException(gantt.getTasks(Convert.ToInt32(projectref)).ToList().Count().ToString(),"Tasks count");
                if (loadRequest.resources != null) loadResponse.setResources(gantt.getResources());
                if (loadRequest.dependencies != null) loadResponse.setDependencies(gantt.getDependencies());
                //LogExceptions.LogException(gantt.getResources().ToList().Count.ToString(), "Resources");
                //if (gantt.getDependencies(Convert.ToInt32(projectref)) != null)
                //{
                //    if (loadRequest.dependencies != null) loadResponse.setDependencies(gantt.getDependencies(Convert.ToInt32(projectref)));
                //}
                // LogExceptions.LogException(gantt.getDependencies().ToList().Count.ToString(), "Dependencies");

                // put current server revision to the response
                loadResponse.revision = gantt.getRevision();
                //LogExceptions.LogException(gantt.getRevision().ToString(), "Revision");
                // just in case we make any changes during load request processing
                gantt.context.SaveChanges();
                // var result = Content(JsonConvert.SerializeObject(loadResponse), "application/json");
                //LogExceptions.LogException(result.Content.ToString(), "Revision");
                return Content(JsonConvert.SerializeObject(loadResponse), "application/json");
            }
            catch (Exception e)
            {
                LogExceptions.WriteExceptionLog(e);
                return Content(JsonConvert.SerializeObject(new ErrorResponse(e, requestId)), "application/json");
            }
        }

        protected SyncStoreResponse AddModifiedRows(Gantt gantt, string table, SyncStoreResponse resp)
        {
            if (gantt.HasUpdatedRows(table))
            {
                if (resp == null) resp = new SyncStoreResponse();
                var rows = gantt.GetUpdatedRows(table);
                resp.rows = resp.rows != null ? resp.rows.Concat(rows).ToList() : rows;
            }
            if (gantt.HasRemovedRows(table))
            {
                if (resp == null) resp = new SyncStoreResponse();
                var removed = gantt.GetRemovedRows(table);
                resp.removed = resp.removed != null ? resp.removed.Concat(removed).ToList() : removed;
            }
            return resp;
        }

        /// <summary>
        /// Sync response handler.
        /// </summary>
        /// <returns>JSON encoded response.</returns>
        [System.Web.Mvc.HttpPost]
        public ActionResult Sync(int id)
        {
            //var ProjectReference = id;
            ulong? requestId = null;
            GanttSyncRequest syncRequest = null;

            try
            {
                string json = getPostBody();

                var gantt = new Gantt();

                // decode request object
                try
                {
                    syncRequest = JsonConvert.DeserializeObject<GanttSyncRequest>(json, new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = dateFormat });
                }
                catch (Exception)
                {
                    throw new Exception("Invalid sync JSON");
                }

                // initialize phantom to real Id maps
                gantt.InitRowsHolders();

                // get request identifier
                requestId = syncRequest.requestId;

                // initialize response object
                var syncResponse = new GanttSyncResponse(requestId);

                // Here we reject client's changes if we suspect that they are out-dated
                // considering difference between server and client revisions.
                // You can get rid of this call if you don't need such behavior.
                gantt.checkRevision(syncRequest.revision);

                // if a corresponding store modified data are provided then we handle them

                // first let's process added and updated records 

                CalendarSyncHandler calendarHandler = null;
                if (syncRequest.calendars != null)
                {
                    calendarHandler = new CalendarSyncHandler(gantt, dateFormat);
                    syncResponse.calendars = calendarHandler.Handle(syncRequest.calendars, CalendarSyncHandler.Rows.AddedAndUpdated);
                }
                ResourceSyncHandler resourcesHandler = null;
                if (syncRequest.resources != null)
                {
                    resourcesHandler = new ResourceSyncHandler(gantt);
                    syncResponse.resources = resourcesHandler.Handle(syncRequest.resources, ResourceSyncHandler.Rows.AddedAndUpdated);
                }
                TaskSyncHandler taskHandler = null;
                if (syncRequest.tasks != null)
                {
                    taskHandler = new TaskSyncHandler(gantt, dateFormat);
                    syncResponse.tasks = taskHandler.Handle(syncRequest.tasks, TaskSyncHandler.Rows.AddedAndUpdated);
                }
                AssignmentSyncHandler assignmentHandler = null;
                if (syncRequest.assignments != null)
                {
                    assignmentHandler = new AssignmentSyncHandler(gantt);
                    syncResponse.assignments = assignmentHandler.Handle(syncRequest.assignments, AssignmentSyncHandler.Rows.AddedAndUpdated);
                }
                DependencySyncHandler dependencyHandler = null;
                if (syncRequest.dependencies != null)
                {
                    dependencyHandler = new DependencySyncHandler(gantt);
                    syncResponse.dependencies = dependencyHandler.Handle(syncRequest.dependencies, DependencySyncHandler.Rows.AddedAndUpdated);
                }

                // then let's process records removals

                if (syncRequest.dependencies != null)
                    syncResponse.dependencies = dependencyHandler.HandleRemoved(syncRequest.dependencies, syncResponse.dependencies);

                if (syncRequest.assignments != null)
                    syncResponse.assignments = assignmentHandler.HandleRemoved(syncRequest.assignments, syncResponse.assignments);

                if (syncRequest.tasks != null)
                    syncResponse.tasks = taskHandler.HandleRemoved(syncRequest.tasks, syncResponse.tasks);

                if (syncRequest.resources != null)
                    syncResponse.resources = resourcesHandler.HandleRemoved(syncRequest.resources, syncResponse.resources);

                if (syncRequest.calendars != null)
                    syncResponse.calendars = calendarHandler.HandleRemoved(syncRequest.calendars, syncResponse.calendars);

                // we also return implicit modifications made by server
                // (implicit records updates/removals caused by data references)

                syncResponse.calendars = AddModifiedRows(gantt, "calendars", syncResponse.calendars);
                syncResponse.tasks = AddModifiedRows(gantt, "tasks", syncResponse.tasks);
                syncResponse.resources = AddModifiedRows(gantt, "resources", syncResponse.resources);
                syncResponse.assignments = AddModifiedRows(gantt, "assignments", syncResponse.assignments);
                syncResponse.dependencies = AddModifiedRows(gantt, "dependencies", syncResponse.dependencies);

                // put current server revision to the response
                syncResponse.revision = gantt.getRevision();

                gantt.context.SaveChanges();

                return Content(JsonConvert.SerializeObject(syncResponse), "application/json");
            }
            catch (Exception e)
            {
                LogExceptions.WriteExceptionLog(e);
                return Content(JsonConvert.SerializeObject(new ErrorResponse(e, requestId)), "application/json");
            }
        }

        /// <summary>
        /// Back-end test handler providing database cleanup.
        /// TODO: WARNING! This code clears the database. Please get rid of this code before running it on production.
        /// </summary>
        /// <returns>Empty string.</returns>
        public string Reset()
        {
            var gantt = new Gantt();

            gantt.Reset();
            gantt.context.SaveChanges();

            return "";
        }

    }
}