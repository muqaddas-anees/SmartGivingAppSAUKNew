<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tjs.aspx.cs" Inherits="DeffinityAppDev.tjs" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    
<%: System.Web.Optimization.Scripts.Render("~/bundles/angularjs") %>
</head>
<body>
    <form id="form1" runat="server">
  
        
    <div ng-app="myApp" ng-controller="myCtrl">
 Your Name: <input type="text" ng-model="test"/>
<hr/>
 Hello {{test || "World"}}!
</div>
         <script>
           
             var app = angular.module('myApp', []);
             app.controller('myCtrl', function ($scope) {
                 $scope.test = "John";
                              });
</script>
    </form>
</body>
</html>
