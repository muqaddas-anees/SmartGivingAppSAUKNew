/*! KEditor v2.0.1 | Copyright (c) 2016-present Kademi (http://kademi.co) */
!(function (t, o) {
    if ("object" == typeof exports && "object" == typeof module) module.exports = o(require("KEditor"), require("jQuery"), require("CKEDITOR"));
    else if ("function" == typeof define && define.amd) define(["KEditor", "jQuery", "CKEDITOR"], o);
    else {
        var e = "object" == typeof exports ? o(require("KEditor"), require("jQuery"), require("CKEDITOR")) : o(t.KEditor, t.jQuery, t.CKEDITOR);
        for (var n in e) ("object" == typeof exports ? exports : t)[n] = e[n];
    }
})("undefined" != typeof self ? self : this, function (t, o, e) {
    return (function (t) {
        var o = {};
        function e(n) {
            if (o[n]) return o[n].exports;
            var i = (o[n] = { i: n, l: !1, exports: {} });
            return t[n].call(i.exports, i, i.exports, e), (i.l = !0), i.exports;
        }
        return (
            (e.m = t),
            (e.c = o),
            (e.d = function (t, o, n) {
                e.o(t, o) || Object.defineProperty(t, o, { enumerable: !0, get: n });
            }),
            (e.r = function (t) {
                "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(t, Symbol.toStringTag, { value: "Module" }), Object.defineProperty(t, "__esModule", { value: !0 });
            }),
            (e.t = function (t, o) {
                if ((1 & o && (t = e(t)), 8 & o)) return t;
                if (4 & o && "object" == typeof t && t && t.__esModule) return t;
                var n = Object.create(null);
                if ((e.r(n), Object.defineProperty(n, "default", { enumerable: !0, value: t }), 2 & o && "string" != typeof t))
                    for (var i in t)
                        e.d(
                            n,
                            i,
                            function (o) {
                                return t[o];
                            }.bind(null, i)
                        );
                return n;
            }),
            (e.n = function (t) {
                var o =
                    t && t.__esModule
                        ? function () {
                              return t.default;
                          }
                        : function () {
                              return t;
                          };
                return e.d(o, "a", o), o;
            }),
            (e.o = function (t, o) {
                return Object.prototype.hasOwnProperty.call(t, o);
            }),
            (e.p = ""),
            e((e.s = 7))
        );
    })([
        function (o, e) {
            o.exports = t;
        },
        function (t, e) {
            t.exports = o;
        },
        function (t, o) {
            t.exports = e;
        },
        ,
        function (t, o, e) {},
        function (t, o, e) {},
        ,
        function (t, o, e) {
            "use strict";
            e.r(o);
            var n = e(0),
                i = e.n(n);
            i.a.components.audio = {
                settingEnabled: !0,
                settingTitle: "Audio Settings",
                init: function (t, o, e, n) {
                    var i = e.find(".keditor-component-content");
                    0 === i.find(".audio-wrapper").length && i.wrapInner('<div class="audio-wrapper"></div>');
                },
                initSettingForm: function (t, o) {
                    t.append(
                        '<form class="form-horizontal">     <div class="form-group">         <label class="col-sm-12">Audio file</label>         <div class="col-sm-12">             <div class="audio-toolbar">                 <a href="#" class="btn-audio-upload btn btn-sm btn-primary"><i class="fa fa-upload"></i></a>                 <input class="audio-upload" type="file" style="display: none" />             </div>         </div>     </div>     <div class="form-group">         <label class="col-sm-12">Autoplay</label>         <div class="col-sm-12">             <input type="checkbox" class="audio-autoplay" />         </div>     </div>     <div class="form-group">         <label class="col-sm-12">Show Controls</label>         <div class="col-sm-12">             <input type="checkbox" class="audio-controls" checked />         </div>     </div>     <div class="form-group">         <label class="col-sm-12">Width (%)</label>         <div class="col-sm-12">             <input type="number" min="20" max="100" class="form-control audio-width" value="100" />         </div>     </div></form>'
                    );
                    var e = t.find(".audio-upload");
                    t
                        .find(".btn-audio-upload")
                        .off("click")
                        .on("click", function (t) {
                            t.preventDefault(), e.trigger("click");
                        }),
                        e.off("change").on("change", function () {
                            var t = this.files[0];
                            /audio/.test(t.type) ? o.getSettingComponent().find("audio").attr("src", URL.createObjectURL(t)) : alert("Your selected file is not an audio file!");
                        }),
                        t.find(".audio-autoplay").on("click", function () {
                            o.getSettingComponent().find("audio").prop("autoplay", this.checked);
                        }),
                        t.find(".audio-controls").on("click", function () {
                            o.getSettingComponent().find("audio").prop("controls", this.checked);
                        }),
                        t.find(".audio-width").on("change", function () {
                            var t = o.getSettingComponent().find("audio");
                            t.parent().attr("data-width", this.value), t.css("width", this.value + "%");
                        });
                },
                showSettingForm: function (t, o, e) {
                    var n = o.find("audio"),
                        i = n.parent(),
                        a = t.find(".audio-autoplay"),
                        l = t.find(".audio-controls"),
                        s = t.find(".audio-width");
                    a.prop("checked", !!n.attr("autoplay")), l.prop("checked", !!n.attr("controls")), s.val(i.attr("data-width") || 100);
                },
            };
            e(4);
            var a,
                l,
                s = e(1),
                r = e.n(s);
            (i.a.components.form = {
                emptyContent: '<p class="text-muted lead text-center"><br />[No form content]<br /><br /></p>',
                renderForm: function (t) {
                    var o = t.find(".form-content"),
                        e = r()("<div />");
                    e.formRender({ dataType: "json", formData: l.actions.getData("json") }),
                        o.html(e.html()),
                        o.hasClass("form-horizontal") &&
                            o.children("div").each(function () {
                                var t = r()(this),
                                    e = o.attr("data-grid") || "4-8";
                                if (((e = e.split("-")), t.attr("class")))
                                    if (t.hasClass("fb-button")) t.find("button").wrap('<div class="col-sm-'.concat(e[1], " col-sm-offset-").concat(e[0], '"></div>'));
                                    else {
                                        var n = t.children("label"),
                                            i = t.children("input, select, textarea"),
                                            a = t.children("div");
                                        n.addClass("control-label col-sm-".concat(e[0])), a.length > 0 ? a.addClass("col-sm-".concat(e[1])) : i.addClass("form-control").wrap('<div class="col-sm-'.concat(e[1], '"></div>'));
                                    }
                            });
                },
                initModal: function (t) {
                    var o = this;
                    (a = t.initModal("keditor-modal-form")).find(".keditor-modal-title").html("Design form"),
                        a.css({ visibility: "hidden", display: "block", opacity: 1 }),
                        a.find(".keditor-modal-body").append('\n            <div class="form-builder-area-wrapper">\n                <div class="form-builder-area"></div>\n            </div>\n        '),
                        (l = a
                            .find(".form-builder-area")
                            .formBuilder({ showActionButtons: !1, dataType: "json", disableFields: ["autocomplete", "paragraph", "header"], disabledAttrs: ["access"], typeUserDisabledAttrs: { "checkbox-group": ["toggle", "inline"] } })),
                        a
                            .find(".keditor-modal-footer")
                            .html(
                                '\n            <button type="button" class="keditor-ui keditor-btn keditor-btn-default keditor-modal-close">Close</button>\n            <button type="button" class="keditor-ui keditor-btn keditor-btn-primary btn-save-form">Save</button>\n        '
                            ),
                        a.find(".btn-save-form").on("click", function (e) {
                            e.preventDefault();
                            var n = t.getSettingComponent();
                            n.find(".form-data").html(l.actions.getData("json")), o.renderForm(n), t.hideModal(a);
                        }),
                        setTimeout(function () {
                            a.css({ visibility: "", display: "", opacity: "" });
                        }, 500);
                },
                init: function (t, o, e, n) {
                    var i = e.find(".keditor-component-content"),
                        l = e.find(".form-content");
                    0 === e.find(".form-data").length && i.append('<div class="form-data" style="display: none !important;"></div>'),
                        0 === l.length && i.append('<form class="form-content">'.concat(this.emptyContent, "</form>")),
                        a || this.initModal(n);
                },
                settingEnabled: !0,
                settingTitle: "Form Settings",
                initSettingForm: function (t, o) {
                    var e = this;
                    t.html(
                        '\n            <div class="form-horizontal">\n                <div class="form-group">\n                    <div class="col-sm-12">\n                       <button class="btn btn-primary btn-block btn-design-form" type="button"><i class="fa fa-paint-brush"></i> Design form</button>\n                    </div>\n                </div>\n                <div class="form-group">\n                    <label class="col-sm-12">Action</label>\n                    <div class="col-sm-12">\n                        <input type="text" class="form-control txt-form-action" />\n                    </div>\n                </div>\n                <div class="form-group">\n                    <label class="col-sm-12">Method</label>\n                    <div class="col-sm-12">\n                        <select class="form-control select-method">\n                            <option value="get">Get</option>\n                            <option value="post">Post</option>\n                            <option value="put">Put</option>\n                            <option value="delete">Delete</option>\n                        </select>\n                    </div>\n                </div>\n                <div class="form-group">\n                    <label class="col-sm-12">Enctype</label>\n                    <div class="col-sm-12">\n                        <select class="form-control select-enctype">\n                            <option value="text/plain">text/plain</option>\n                            <option value="multipart/form-data">multipart/form-data</option>\n                            <option value="application/x-www-form-urlencoded">application/x-www-form-urlencoded</option>\n                        </select>\n                    </div>\n                </div>\n                <div class="form-group">\n                    <label class="col-sm-12">Layout</label>\n                    <div class="col-sm-12">\n                        <select class="form-control select-layout">\n                            <option value="">Normal</option>\n                            <option value="form-horizontal">Horizontal</option>\n                            <option value="form-inline">Inline</option>\n                        </select>\n                    </div>\n                </div>\n                <div class="form-group select-grid-wrapper">\n                    <label class="col-sm-12">Grid setting</label>\n                    <div class="col-sm-12">\n                        <select class="form-control select-grid">\n                            <option value="2-10">col-2 col-10</option>\n                            <option value="3-9">col-3 col-9</option>\n                            <option value="4-8">col-4 col-8</option>\n                            <option value="5-7">col-5 col-7</option>\n                            <option value="6-6">col-6 col-6</option>\n                        </select>\n                        <small class="help-block">This setting is for width of label and control with number of cols as unit</small>\n                    </div>\n                </div>\n            </div>\n        '
                    ),
                        t.find(".btn-design-form").on("click", function (t) {
                            t.preventDefault();
                            var e = o.getSettingComponent();
                            l.actions.setData(e.find(".form-data").html()), o.showModal(a);
                        }),
                        t.find(".txt-form-action").on("change", function () {
                            o.getSettingComponent().find(".form-content").attr("action", this.value);
                        }),
                        t.find(".select-method").on("change", function () {
                            o.getSettingComponent().find(".form-content").attr("action", this.value);
                        }),
                        t.find(".select-enctype").on("change", function () {
                            o.getSettingComponent().find(".form-content").attr("enctype", this.value);
                        }),
                        t.find(".select-layout").on("change", function () {
                            var n = o.getSettingComponent(),
                                i = n.find(".form-content");
                            i.removeClass("form-inline form-horizontal"), this.value && i.addClass(this.value), e.renderForm(n), t.find(".select-grid-wrapper").css("display", "form-horizontal" === this.value ? "block" : "none");
                        }),
                        t.find(".select-grid").on("change", function () {
                            var t = o.getSettingComponent();
                            t.find(".form-content").attr("data-grid", this.value), e.renderForm(t);
                        });
                },
                showSettingForm: function (t, o, e) {
                    var n = o.find(".form-content"),
                        i = "";
                    n.hasClass("form-inline") ? (i = "form-inline") : n.hasClass("form-horizontal") && (i = "form-horizontal"),
                        t.find(".txt-form-action").val(n.attr("action") || ""),
                        t.find(".select-method").val(n.attr("method") || "get"),
                        t.find(".select-enctype").val(n.attr("enctype")),
                        t.find(".select-layout").val(i),
                        t.find(".select-grid-wrapper").css("display", "form-horizontal" === i ? "block" : "none"),
                        t.find(".select-grid").val(n.attr("data-grid") || "4-8");
                },
            }),
                (i.a.components.googlemap = {
                    init: function (t, o, e, n) {
                        var i = e.find("iframe"),
                            a = i.parent();
                        n.initIframeCover(i, a);
                    },
                    settingEnabled: !0,
                    settingTitle: "Google Map Settings",
                    initSettingForm: function (t, o) {
                        t.append(
                            '<form class="form-horizontal">   <div class="form-group">       <div class="col-sm-12">           <button type="button" class="btn btn-block btn-primary btn-googlemap-edit">Update Map</button>       </div>   </div>   <div class="form-group">       <label class="col-sm-12">Aspect Ratio</label>       <div class="col-sm-12">           <button type="button" class="btn btn-sm btn-default btn-googlemap-169">16:9</button>           <button type="button" class="btn btn-sm btn-default btn-googlemap-43">4:3</button>       </div>   </div></form>'
                        ),
                            t.find(".btn-googlemap-edit").on("click", function (t) {
                                t.preventDefault();
                                var e = prompt("Please enter Google Map embed code in here:"),
                                    n = r()(e),
                                    i = n.attr("src");
                                n.length > 0 && i && i.length > 0 ? o.getSettingComponent().find(".embed-responsive-item").attr("src", i) : alert("Your Google Map embed code is invalid!");
                            }),
                            t.find(".btn-googlemap-169").on("click", function (t) {
                                t.preventDefault(), o.getSettingComponent().find(".embed-responsive").removeClass("embed-responsive-4by3").addClass("embed-responsive-16by9");
                            }),
                            t.find(".btn-googlemap-43").on("click", function (t) {
                                t.preventDefault(), o.getSettingComponent().find(".embed-responsive").removeClass("embed-responsive-16by9").addClass("embed-responsive-4by3");
                            });
                    },
                }),
                (i.a.components.photo = {
                    init: function (t, o, e, n) {
                        e.children(".keditor-component-content").find("img").css("display", "inline-block");
                       // e.children(".keditor-component-content").find("img").addClass("img-fluid");
                    },
                    settingEnabled: !0,
                    settingTitle: "Photo Settings",
                    initSettingForm: function (t, o) {
                        var e = this;
                        o.options;
                        t.append(
                            '<form class="form-horizontal">   <div class="form-group">       <div class="col-sm-12">           <button type="button" class="btn btn-block btn-primary" id="photo-edit">Change Photo</button>           <input type="file" style="display: none" />       </div>   </div>   <div class="form-group">       <label for="photo-align" class="col-sm-12">Align</label>       <div class="col-sm-12">           <select id="photo-align" class="form-control">               <option value="left">Left</option>               <option value="center">Center</option>               <option value="right">Right</option>           </select>       </div>   </div>   <div class="form-group">       <label for="photo-style" class="col-sm-12">Style</label>       <div class="col-sm-12">           <select id="photo-style" class="form-control">               <option value="">None</option>               <option value="img-rounded">Rounded</option>               <option value="img-circle">Circle</option>               <option value="img-thumbnail">Thumbnail</option>           </select>       </div>   </div>   <div class="form-group">       <label for="photo-responsive" class="col-sm-12">Responsive</label>       <div class="col-sm-12">           <input type="checkbox" id="photo-responsive" />       </div>   </div>   <div class="form-group">       <label for="photo-width" class="col-sm-12">Width</label>       <div class="col-sm-12">           <input type="number" id="photo-width" class="form-control" />       </div>   </div>   <div class="form-group">       <label for="photo-height" class="col-sm-12">Height</label>       <div class="col-sm-12">           <input type="number" id="photo-height" class="form-control" />       </div>   </div></form>'
                        );
                        var n = t.find("#photo-edit"),
                            i = n.next();
                        n.on("click", function (t) {
                            t.preventDefault(), i.trigger("click");
                        }),
                            i.on("change", function () {
                                var n = this.files[0];
                                if (/image/.test(n.type)) {
                                    var i = new FileReader();
                                    i.addEventListener("load", function (n) {
                                        var i = o.getSettingComponent().find("img");
                                        i.attr("src", n.target.result),
                                            i.css({ width: "", height: "" }),
                                            i.load(function () {
                                                e.showSettingForm.call(e, t, o.getSettingComponent(), o);
                                            });
                                    }),
                                        i.readAsDataURL(this.files[0]);
                                } else alert("Your selected file is not photo!");
                            }),
                            t.find("#photo-align").on("change", function () {
                                o.getSettingComponent().find(".photo-panel").css("text-align", this.value);
                            }),
                            t.find("#photo-responsive").on("click", function () {
                                o.getSettingComponent().find("img")[this.checked ? "addClass" : "removeClass"]("img-responsive");
                            }),
                            t.find("#photo-style").on("change", function () {
                                var t = o.getSettingComponent().find("img"),
                                    e = this.value;
                                t.removeClass("img-rounded img-circle img-thumbnail"), e && t.addClass(e);
                            });
                        var a = t.find("#photo-width"),
                            l = t.find("#photo-height");
                        a.on("change", function () {
                            var t = o.getSettingComponent().find("img"),
                                n = +this.value,
                                i = Math.round(n / e.ratio);
                            n <= 0 && ((n = e.width), (i = e.height), (this.value = n)), t.css({ width: n, height: i }), l.val(i);
                        }),
                            l.on("change", function () {
                                var t = o.getSettingComponent().find("img"),
                                    n = +this.value,
                                    i = Math.round(n * e.ratio);
                                n <= 0 && ((i = e.width), (n = e.height), (this.value = n)), t.css({ height: n, width: i }), a.val(i);
                            });
                    },
                    showSettingForm: function (t, o, e) {
                        var n = this,
                            i = t.find("#photo-align"),
                            a = t.find("#photo-responsive"),
                            l = t.find("#photo-width"),
                            s = t.find("#photo-height"),
                            d = t.find("#photo-style"),
                            c = o.find(".photo-panel"),
                            p = c.find("img"),
                            u = c.css("text-align");
                        ("right" === u && "center" === u) || (u = "left"),
                            p.hasClass("img-rounded") ? d.val("img-rounded") : p.hasClass("img-circle") ? d.val("img-circle") : p.hasClass("img-thumbnail") ? d.val("img-thumbnail") : d.val(""),
                            i.val(u),
                            a.prop("checked", p.hasClass("img-responsive")),
                            l.val(p.width()),
                            s.val(p.height()),
                            r()("<img />")
                                .attr("src", p.attr("src"))
                                .load(function () {
                                    (n.ratio = this.width / this.height), (n.width = this.width), (n.height = this.height);
                                });
                    },
                });
            e(5);
            var d = e(2),
                c = e.n(d);
            (c.a.disableAutoInline = !0),
                (c.a.dom.element.prototype.scrollIntoView = function () {}),
                (c.a.dom.selection.prototype.scrollIntoView = function () {}),
                (c.a.dom.range.prototype.scrollIntoView = function () {}),
                (i.a.components.text = {
                    options: {
                        toolbarGroups: [
                            { name: "document", groups: ["mode", "document", "doctools"] },
                            { name: "editing", groups: ["find", "selection", "spellchecker", "editing"] },
                            { name: "forms", groups: ["forms"] },
                            { name: "basicstyles", groups: ["basicstyles", "cleanup"] },
                            { name: "paragraph", groups: ["list", "indent", "blocks", "align", "bidi", "paragraph"] },
                            { name: "links", groups: ["links"] },
                            { name: "insert", groups: ["insert"] },
                            "/",
                            { name: "clipboard", groups: ["clipboard", "undo"] },
                            { name: "styles", groups: ["styles"] },
                            { name: "colors", groups: ["colors"] },
                        ],
                        title: !1,
                        allowedContent: !0,
                        bodyId: "editor",
                        templates_replaceContent: !1,
                        enterMode: "P",
                        forceEnterMode: !0,
                        format_tags: "p;h1;h2;h3;h4;h5;h6",
                        removePlugins: "table,magicline,tableselection,tabletools",
                        removeButtons:
                            "Save,NewPage,Preview,Print,Templates,PasteText,PasteFromWord,Find,Replace,SelectAll,Scayt,Form,HiddenField,ImageButton,Button,Select,Textarea,TextField,Radio,Checkbox,Outdent,Indent,Blockquote,CreateDiv,Language,Table,HorizontalRule,Smiley,SpecialChar,PageBreak,Iframe,Styles,BGColor,Maximize,About,ShowBlocks,BidiLtr,BidiRtl,Flash,Image,Subscript,Superscript,Anchor",
                        minimumChangeMilliseconds: 100,
                    },
                    init: function (t, o, e, n) {
                        var i = n.options,
                            a = e.children(".keditor-component-content");
                        a.prop("contenteditable", !0),
                            a.on("input", function (a) {
                                "function" == typeof i.onComponentChanged && i.onComponentChanged.call(n, a, e),
                                    "function" == typeof i.onContainerChanged && i.onContainerChanged.call(n, a, o, t),
                                    "function" == typeof i.onContentChanged && i.onContentChanged.call(n, a, t);
                            });
                        var l = c.a.inline(a[0], this.options);
                        l.on("instanceReady", function () {
                            $("#cke_" + a.attr("id")).appendTo(n.wrapper), "function" == typeof i.onComponentReady && i.onComponentReady.call(t, e, l);
                        }),
                            l.on(
                                "key",
                                function (t) {
                                    ((t.data.domEvent.$.ctrlKey && 86 === t.data.domEvent.$.keyCode) || 13 === t.data.domEvent.$.keyCode) &&
                                        (console.log("Dont scroll!!"), n.iframeBody.scrollTop($(l.element.$).offset().top), setTimeout(function () {}, 10));
                                },
                                l
                            );
                    },
                    getContent: function (t, o) {
                        var e = t.find(".keditor-component-content"),
                            n = e.attr("id"),
                            i = c.a.instances[n];
                        return i ? i.getData() : e.html();
                    },
                    destroy: function (t, o) {
                        var e = t.find(".keditor-component-content").attr("id");
                        c.a.instances[e] && c.a.instances[e].destroy();
                    },
                }),
                (i.a.components.video = {
                    init: function (t, o, e, n) {
                        var i = e.children(".keditor-component-content").find("video");
                        i.parent().is(".video-wrapper") || i.wrap('<div class="video-wrapper"></div>');
                    },
                    getContent: function (t, o) {
                        var e = t.children(".keditor-component-content");
                        return e.find("video").unwrap(), e.html();
                    },
                    settingEnabled: !0,
                    settingTitle: "Video Settings",
                    initSettingForm: function (t, o) {
                        t.append(
                            '\n            <form class="form-horizontal">\n                <div class="form-group">\n                    <label for="video-input" class="col-sm-12">Video file</label>\n                    <div class="col-sm-12">\n                        <div class="video-toolbar">\n                            <a href="#" class="btn-video-input btn btn-sm btn-primary"><i class="fa fa-upload"></i></a>\n                            <input class="video-input" type="file" style="display: none" />\n                        </div>\n                    </div>\n                </div>\n                <div class="form-group">\n                    <label for="video-autoplay" class="col-sm-12">Autoplay</label>\n                    <div class="col-sm-12">\n                        <input type="checkbox" class="video-autoplay" />\n                    </div>\n                </div>\n                <div class="form-group">\n                    <label for="video-loop" class="col-sm-12">Loop</label>\n                    <div class="col-sm-12">\n                        <input type="checkbox" class="video-loop" />\n                    </div>\n                </div>\n                <div class="form-group">\n                    <label for="video-controls" class="col-sm-12">Show Controls</label>\n                    <div class="col-sm-12">\n                        <input type="checkbox" class="video-controls" checked />\n                    </div>\n                </div>\n                <div class="form-group">\n                    <label for="" class="col-sm-12">Ratio</label>\n                    <div class="col-sm-12">\n                        <input type="radio" name="video-radio" class="video-ratio" value="4/3" checked /> 4:3\n                    </div>\n                    <div class="col-sm-12">\n                        <input type="radio" name="video-radio" class="video-ratio" value="16/9" /> 16:9\n                    </div>\n                </div>\n                <div class="form-group">\n                    <label for="video-width" class="col-sm-12">Width (px)</label>\n                    <div class="col-sm-12">\n                        <input type="number" class="video-width form-control" min="320" max="1920" value="320" />\n                    </div>\n                </div>\n            </form>\n        '
                        );
                        var e = t.find(".video-input");
                        t.find(".btn-video-input").on("click", function (t) {
                            t.preventDefault(), e.trigger("click");
                        }),
                            e.on("change", function () {
                                var t = this.files[0],
                                    e = o.getSettingComponent().find("video");
                                /video/.test(t.type) ? e.attr("src", URL.createObjectURL(t)) : alert("Your selected file is not an video file!");
                            }),
                            t.find(".video-autoplay").on("click", function () {
                                o.getSettingComponent().find("video").prop("autoplay", this.checked);
                            }),
                            t.find(".video-loop").on("click", function () {
                                o.getSettingComponent().find("video").prop("loop", this.checked);
                            }),
                            t.find(".video-ratio").on("click", function () {
                                o.getSettingComponent().find("video").attr("data-ratio", this.value), t.find(".video-width").trigger("change");
                            }),
                            t.find(".video-controls").on("click", function () {
                                o.getSettingComponent().find("video").prop("controls", this.checked);
                            }),
                            t.find(".video-width").on("change", function () {
                                var t = o.getSettingComponent().find("video"),
                                    e = "16/9" === t.attr("data-ratio") ? 16 / 9 : 4 / 3,
                                    n = this.value / e;
                                t.attr("width", this.value), t.attr("height", n);
                            });
                    },
                    showSettingForm: function (t, o, e) {
                        var n = o.find("video");
                        t.find(".video-autoplay").prop("checked", n.prop("autoplay")),
                            t.find(".video-loop").prop("checked", n.prop("loop")),
                            t
                                .find(".video-ratio")
                                .prop("checked", !1)
                                .filter('[value="' + n.attr("data-ratio") + '"]')
                                .prop("checked", !0),
                            t.find(".video-controls").prop("checked", n.prop("controls")),
                            t.find(".video-width").val(n.attr("width"));
                    },
                }),
                (i.a.components.vimeo = {
                    init: function (t, o, e, n) {
                        var i = e.find("iframe"),
                            a = i.parent();
                        n.initIframeCover(i, a);
                    },
                    settingEnabled: !0,
                    settingTitle: "Vimeo Settings",
                    initSettingForm: function (t, o) {
                        t.append(
                            '<form class="form-horizontal">   <div class="form-group">       <div class="col-sm-12">           <button type="button" class="btn btn-block btn-primary btn-vimeo-edit">Change Video</button>       </div>   </div>   <div class="form-group">       <label class="col-sm-12">Autoplay</label>       <div class="col-sm-12">           <input type="checkbox" id="vimeo-autoplay" />       </div>   </div>   <div class="form-group">       <label class="col-sm-12">Aspect Ratio</label>       <div class="col-sm-12">           <button type="button" class="btn btn-sm btn-default btn-vimeo-169">16:9</button>           <button type="button" class="btn btn-sm btn-default btn-vimeo-43">4:3</button>       </div>   </div></form>'
                        ),
                            t.find(".btn-vimeo-edit").on("click", function (t) {
                                t.preventDefault();
                                var e = prompt("Please enter Vimeo URL in here:").match(/https?:\/\/(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|)(\d+)(?:$|\/|\?)/);
                                e && e[1]
                                    ? o
                                          .getSettingComponent()
                                          .find(".embed-responsive-item")
                                          .attr("src", "https://player.vimeo.com/video/" + e[1] + "?byline=0&portrait=0&badge=0")
                                    : alert("Your Vimeo URL is invalid!");
                            }),
                            t.find(".btn-vimeo-169").on("click", function (t) {
                                t.preventDefault(), o.getSettingComponent().find(".embed-responsive").removeClass("embed-responsive-4by3").addClass("embed-responsive-16by9");
                            }),
                            t.find(".btn-vimeo-43").on("click", function (t) {
                                t.preventDefault(), o.getSettingComponent().find(".embed-responsive").removeClass("embed-responsive-16by9").addClass("embed-responsive-4by3");
                            });
                        var e = t.find("#vimeo-autoplay");
                        e.on("click", function () {
                            var t = o.getSettingComponent().find(".embed-responsive-item"),
                                n = t.attr("src").replace(/(\?.+)+/, "") + "?byline=0&portrait=0&badge=0&autoplay=" + (e.is(":checked") ? 1 : 0);
                            t.attr("src", n);
                        });
                    },
                    showSettingForm: function (t, o, e) {
                        var n = o.find(".embed-responsive-item"),
                            i = t.find("#vimeo-autoplay"),
                            a = n.attr("src");
                        i.prop("checked", -1 !== a.indexOf("autoplay=1"));
                    },
                }),
                (i.a.components.youtube = {
                    init: function (t, o, e, n) {
                        var i = e.find("iframe"),
                            a = i.parent();
                        n.initIframeCover(i, a);
                    },
                    settingEnabled: !0,
                    settingTitle: "Youtube Settings",
                    initSettingForm: function (t, o) {
                        t.append(
                            '<form class="form-horizontal">   <div class="form-group">       <div class="col-sm-12">           <button type="button" class="btn btn-block btn-primary btn-youtube-edit">Change Video</button>       </div>   </div>   <div class="form-group">       <label class="col-sm-12">Autoplay</label>       <div class="col-sm-12">           <input type="checkbox" id="youtube-autoplay" />       </div>   </div>   <div class="form-group">       <label class="col-sm-12">Aspect Ratio</label>       <div class="col-sm-12">           <button type="button" class="btn btn-sm btn-default btn-youtube-169">16:9</button>           <button type="button" class="btn btn-sm btn-default btn-youtube-43">4:3</button>       </div>   </div></form>'
                        ),
                            t.find(".btn-youtube-edit").on("click", function (t) {
                                t.preventDefault();
                                var e = prompt("Please enter Youtube URL in here:").match(/^(?:http(?:s)?:\/\/)?(?:www\.)?(?:m\.)?(?:youtu\.be\/|youtube\.com\/(?:(?:watch)?\?(?:.*&)?v(?:i)?=|(?:embed|v|vi|user)\/))([^\?&\"'>]+)/);
                                e && e[1]
                                    ? o
                                          .getSettingComponent()
                                          .find(".embed-responsive-item")
                                          .attr("src", "https://www.youtube.com/embed/" + e[1])
                                    : alert("Your Youtube URL is invalid!");
                            }),
                            t.find(".btn-youtube-169").on("click", function (t) {
                                t.preventDefault(), o.getSettingComponent().find(".embed-responsive").removeClass("embed-responsive-4by3").addClass("embed-responsive-16by9");
                            }),
                            t.find(".btn-youtube-43").on("click", function (t) {
                                t.preventDefault(), o.getSettingComponent().find(".embed-responsive").removeClass("embed-responsive-16by9").addClass("embed-responsive-4by3");
                            });
                        var e = t.find("#youtube-autoplay");
                        e.on("click", function () {
                            var t = o.getSettingComponent().find(".embed-responsive-item"),
                                n = t.attr("src").replace(/(\?.+)+/, "") + "?autoplay=" + (e.is(":checked") ? 1 : 0);
                            t.attr("src", n);
                        });

                        var e = t.find(".apply-youtube");
                       e.on("click", function () {
                            alert("now");
                        });
                    },
                    showSettingForm: function (t, o, e) {
                        var n = o.find(".embed-responsive-item"),
                            i = t.find("#youtube-autoplay"),
                            a = n.attr("src");
                        i.prop("checked", -1 !== a.indexOf("autoplay=1"));
                    },
                });
        },
    ]);
});


$(document).ready(function () {
    //alert('custom load');

    //var e = t.find(".apply-youtube");
    //e.on("click", function () {
    //    var t = o.getSettingComponent().find(".embed-responsive-item"),
    //        n = t.attr("src").replace(/(\?.+)+/, "") + "?autoplay=" + (e.is(":checked") ? 1 : 0);
    //    t.attr("src", n);
    //});
    $(".apply-youtube").on("click", function () {
        alert("now");
    });
    //$(".apply-youtube").click(function () {
    //    debugger;
    //    console.log('apply youtube');
    //    alert('test');
    //    debugger;
    //    alert($(this).closest('input .text-apply-youtube').val());
    //});
});


//# sourceMappingURL=keditor-components.js.map
