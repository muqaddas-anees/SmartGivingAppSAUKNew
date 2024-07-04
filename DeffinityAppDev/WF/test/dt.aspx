<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dt.aspx.cs" Inherits="DeffinityAppDev.WF.test.dt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <link href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "test.asmx/GetStudents",
                success: function (data) {
                    var datatableVariable = $('#studentTable').DataTable({
                        "bFilter": false,
                        "bInfo": false,
                        "bLengthChange": false,
                        data: data,
                        columns: [
                            { 'data': 'ID' },
                            { 'data': 'Name' },
                            { 'data': 'Email' },
                            //{
                            //    'data': 'feesPaid', 'render': function (feesPaid) {
                            //        return '$ ' + feesPaid;
                            //    }
                            //},
                            //{ 'data': 'gender' },
                            { 'data': 'Postcode' },
                            { 'data': 'Telephone' }
                            //,
                            //{
                            //    'data': 'dateOfBirth', 'render': function (date) {
                            //        var date = new Date(parseInt(date.substr(6)));
                            //        var month = date.getMonth() + 1;
                            //        return date.getDate() + "/" + month + "/" + date.getFullYear();
                            //    }
                            //}

                        ]
                    });
                   
                }
            });

        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table id="studentTable" class="table table-responsive table-hover">  
                <thead>  
                    <tr>  
                        <th>ID</th>  
                        <th>Name</th>  
                        <th>Email</th>  
                        <th>Postcode</th>  
                        <th>Telephone</th>  
                    </tr>  
                </thead>  
               
            </table>  
    </div>
    </form>
</body>
</html>
