app.service("myService", function ($http) {
    debugger;
    this.GetStudents() = function () {
        return $http.get("Employee/Create");
    };

});