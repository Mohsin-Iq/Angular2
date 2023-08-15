app.controller("EmpCtrl", function ($scope, myService) {

    GetStudents();

    function GetStudents() {

        debugger;
        var getStudents = myService.GetStudents();
        getStudents.then(function (emp) {
            $scope.employees = emp.data;
        }, function () {
            alert('Data not found');
        });
    }
});