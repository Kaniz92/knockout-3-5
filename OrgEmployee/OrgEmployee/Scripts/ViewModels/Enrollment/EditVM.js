$(function () {
    ko.applyBindings(EditVM);
});

var InitialData = MyNamespace.InitialData;

var EditVM = {
 
    Band: ko.observable(InitialData.Band),
    Departments: ko.observableArray(MyNamespace.Departments),
    Department: ko.observable(),
    DepartmentID: ko.observable(InitialData.DepartmentID),
    EmployeeID: ko.observable(InitialData.EmployeeID),
    Employees: ko.observableArray(MyNamespace.Employees),
    Employee: ko.observable(),
    EnrollmentID: ko.observable(InitialData.EnrollmentID),

    EditEmployee: function () {
        $.ajax({
            url: '/Enrollment/Edit',
            type: 'post',
            dataType: 'json',
            data: ko.toJSON(this),
            contentType: 'application/json',
            success: function (result) {
            },
            error: function (err) {
                console.log("Error: " + err);
            },
            complete: function () {
                window.location.href = '/Enrollment/Index/';
            }
        });
    }
};