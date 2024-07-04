



app.controller("SummaryController", function ($scope, $http) {
    $http.get('../../api/project/ProjectsBasicSummary').
      success(function (data, status, headers, config) {

          var newData = jQuery.parseJSON(data);

          $scope.totalPendingprojects = newData.totalPendingprojects;
          $scope.liveprojects = newData.liveprojects;
          $scope.totalbudget = newData.totalbudget;
          $scope.totallivebudget = newData.totallivebudget;
          $scope.totalmembers = newData.totalmembers;
          $scope.totalactivemembers = newData.totalactivemembers;
          $scope.totalissues = newData.totalissues;
          $scope.totalactiveissues = newData.totalactiveissues;
      }).
      error(function (data, status, headers, config) {
          //alert("erro");
      });
});
app.controller("ProjectDataController", function ($scope, $http) {
    $scope.projectlist_loading = true;
    $scope.projectlist_nodata = false;
    debugger;
    $http.get('../../api/project/ProjectsDashboardSelect').
      success(function (data, status, headers, config) {
          debugger;
          //var newData = jQuery.parseJSON(data);
          $scope.projects = data;
          if ($scope.projects.length == 0) {
              $scope.projectlist_nodata = true;
          }
          else {
              $scope.projectlist_nodata = false;
          }
          //$scope.projectselect(436, 'sample test');
      }).
      error(function (data, status, headers, config) {
          // alert("erro");
      }).finally(function () {
          $scope.projectlist_loading = false;

      });
    //event - select a project
    $scope.projectselect = function (projectreference, projecttitle) {
       
        $scope.projecttitle = projecttitle;
        $scope.projectreference = projectreference;
        loadChart(projectreference);

        //Load tasks
        $scope.tasks_loading = true;
        $scope.tasks_nodata = false;
        $http.get('../../api/project/GetTasks/' + projectreference).
     success(function (data, status, headers, config) {
         //     debugger;
         $scope.projectitems = jQuery.parseJSON(data);
         if ($scope.projectitems.length == 0)
         {
             $scope.tasks_nodata = true;
         }
         else {
             $scope.tasks_nodata = false;
         }
         //    debugger;
     }).
     error(function (data, status, headers, config) {
         //alert("erro");
     })
     .finally(function () {
         $scope.tasks_loading = false;
        
        });

        //Load Active tasks
        $scope.active_loading = true;
        $scope.active_nodata = false;
        $http.get('../../api/project/GetActiveTasks/' + projectreference).
     success(function (data, status, headers, config) {
         // debugger;
         $scope.ActiveTasks = jQuery.parseJSON(data);
         if ($scope.ActiveTasks.length == 0) {
             $scope.active_nodata = true;
         }
         else {
             $scope.active_nodata = false;
         }
         //  debugger;
     }).
     error(function (data, status, headers, config) {
         //alert("erro");
     })
            .finally(function () {
            // Hide loading spinner whether our call succeeded or failed.
                $scope.active_loading = false;
        });

        //Load timesheets
        $scope.timesheet_loading = true;
        $scope.timesheet_nodata = false;
        $http.get('../../api/project/GetTimesheets/' + projectreference).
   success(function (data, status, headers, config) {

       $scope.timesheets = data;
       if ($scope.timesheets.length == 0) {
           $scope.timesheet_nodata = true;
       }
       else {
           $scope.timesheet_nodata = false;
       }
   }).
   error(function (data, status, headers, config) {
       //alert("erro");
   }).finally(function () {
       $scope.timesheet_loading = false;

   });

        //load issues
        $scope.issues_loading = true;
        $scope.issues_nodata = false;
        $http.get('../../api/project/GetIssues/' + projectreference).
   success(function (data, status, headers, config) {
       debugger;
       $scope.issues = data;
       debugger;
       if ($scope.issues.length == 0) {
           $scope.issues_nodata = true;
       }
       else {
           $scope.issues_nodata = false;
       }
   }).
   error(function (data, status, headers, config) {
       // alert("erro");
   }).finally(function () {
       $scope.issues_loading = false;

   });


    }
});



app.filter('jsonDate', function () {
    return function (input, format) {
        //   debugger;
        if (angular.isUndefined(input))
            return;

        // first 6 character is the date
        var date = new Date(parseInt(input.substr(6)));

        // default date format
        if (angular.isUndefined(format))
            format = "MM/DD/YYYY";

        format = format.replace("DD", (date.getDate() < 10 ? '0' : '') + date.getDate()); // Pad with '0' if needed
        format = format.replace("MM", (date.getMonth() < 9 ? '0' : '') + (date.getMonth() + 1)); // Months are zero-based
        format = format.replace("YYYY", date.getFullYear());

        return format;
    };
});