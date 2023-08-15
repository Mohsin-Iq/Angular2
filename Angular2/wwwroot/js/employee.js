

// Script for Load All the Data --------------------------------------------------
function LodData() {
    $.ajax({
        type: "GET",
        url: 'employee/create',
        dataType: "Json",
        contentType: "application/json; charset=utf-8",
        success: function (resopnse) {
            $(".table-summary tbody").empty();

            for (var i = 0; i < resopnse.length; i++) {
                var br = resopnse[i];
                var str = '<tr><td>' + br.employeeID + '</td><td>' + br.name + '</td><td>' + br.email + '</td><td>' + br.phone +
                    '</td><td ><button class="button" onclick="Delete(' + br.employeeID + ')"> Delete </button><button class="button" onclick="return getbyID(' +
                    br.employeeID + ')" data-toggle="modal" data-target="#editModal"> Edit </button></td></tr>';

                $(".table-summary tbody").append(str);
                $('#btnUpdate').hide();

            }
            $('#exampleModal').modal('hide');

            resetPopupFields();
        },
        Error: function () {
            alert: ("Error ");
        }
    });
}
function resetPopupFields() {
    $('#email').val('');
    $('#Phone').val('');
    $('#name').val('');
}
//Delete the Employee---------------------------------------------------------------------
function Delete(employeeID) {
    if (confirm("Are you sure you want to delete this employee?")) {
        debugger;
        $.ajax({
            type: "POST",
            url: 'employee/Delete',
            data: { employeeID: employeeID },
            success: function (response) {
                if (response.success) {
                    var employeeTable = $('#employee tbody');
                    employeeTable.empty();
                    LodData();
                }
            },
            error: function () {
                alert("An error occurred while deleting the employee.");
            }
        });
    }
}
//For Add a new Employee------------------------------------------------------------------
$(".saveData").click(function () {
    var EmployeeID = $("#EmployeeID").val();
    var Name = $("#name").val();
    var Email = $("#email").val();
    var Phone = $("#Phone").val();
    $.ajax({
        type: "POST",
        url: 'Employee/Add',
        dataType: "Json",
        data: { EmployeeID: EmployeeID, Name: Name, Email: Email, Phone: Phone },
        success: function (data) {
            if (data != null) {
                var employeeTable = $('#employee tbody');
                employeeTable.empty();
                LodData();
            }

        },
    });
});

//For EDit the employee-------------------------------------------------------------------
function getbyID(ID) {

    $("#EmployeeID").val();
    $("#name").val();
    $("#email").val();
    $("#Phone").val();
    $.ajax({
        url: "/Employee/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#EmployeeID').val(ID);
            $('#name').val(result.name);
            $('#email').val(result.email);
            $('#Phone').val(result.phone);

            $('#exampleModal').modal('show');
            $('#btnUpdate').show();
            $('.saveData').hide();
        },

    });
    return false;
}
//update the resule-----------------------------------------------------------------------
function Update() {

    var EmployeeID = $("#EmployeeID").val();
    var Name = $("#name").val();
    var Email = $("#email").val();
    var Phone = $("#Phone").val();
    $.ajax({
        type: "POST",
        url: 'Employee/Update',
        dataType: "Json",
        data: { EmployeeID: EmployeeID, Name: Name, Email: Email, Phone: Phone },
        success: function (data) {
            if (data != null) {
                var employeeTable = $('#employee tbody');
                employeeTable.empty();
                LodData();
                debugger;
                $("#EmployeeID").val();
                $("#name").val();
                $("#email").val();
                $("#Phone").val();
            }

        },
    });
}

