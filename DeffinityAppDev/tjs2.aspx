<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tjs2.aspx.cs" Inherits="DeffinityAppDev.tjs2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/angularjs/1.4.8/angular.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Product List</h1>
    <div  ng-app="ProductApp" ng-controller="ProductController">
        <table ng-repeat="product in products" border="1">
            <tr>
                <td>{{product.ID}}</td>
                <td>{{product.ProjectTitle}}</td>
            </tr>

        </table>
    </div>
    
    <div>
    <script>
        function Product() {
            this.ID;
            this.Name;
            this.Category;
            this.Price;
        }
    </script>
        <script>
            var app = angular.module("ProductApp", []);
            app.controller("ProductController", function ($scope, $http) {
                $http.get('api/project').
                  success(function (data, status, headers, config) {
                      debugger;
                      $scope.products = data;

                  }).
                  error(function (data, status, headers, config) {
                      alert("erro");
                  });
            });
        </script>
    </div>
    </form>
</body>
</html>
