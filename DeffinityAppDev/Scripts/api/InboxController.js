


app.controller("InboxDataController", function ($scope, $http) {
    debugger;
    $http.get('../../api/inbox/getlist').
      success(function (data, status, headers, config) {
          debugger;
          //var newData = jQuery.parseJSON(data);
          $scope.inboxmsgs = data;
      }).
      error(function (data, status, headers, config) {
          // alert("erro");
      });
    //event - select a project
});
