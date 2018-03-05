
$(function () {
    ko.applyBindings(IndexVM);
    IndexVM.getEmployees();
});

var IndexVM = {
    Employees: ko.observableArray([]),
    getEmployees: function () {
        var self = this;
        $.ajax({
            type: "GET",
            url: '/Employee/GetAllEmployee',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                self.Employees(data); //Put the response in ObservableArray
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }
        });
    },
    CreateNew: function () {
        window.location.href = '/Employee/Create';
    },
    Back: function(){
        window.location.href = '/Home';
    },
    formatDate: function (textValue) {
        var date = new Date(parseInt(textValue.match(/\d+/)[0])).toLocaleDateString("en-US");
        return date;
    },
    EditEmployee : function (employeeID) {
    window.location.href = '/Employee/Edit/' + employeeID;
    },
    DeleteEmployee: function (employeeID) {
        window.location.href = '/Employee/Delete/' + employeeID;
}
};

