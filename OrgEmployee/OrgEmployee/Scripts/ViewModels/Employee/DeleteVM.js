$(function () {
    ko.applyBindings(DeleteVM);
});

var parsedJSON = MyNamespace.RawModelData;

var DeleteVM = {
    FirstName: ko.observable(parsedJSON.FirstName),
    LastName: ko.observable(parsedJSON.LastName),
    JoiningDate: ko.observable(new Date(parseInt(parsedJSON.JoiningDate.match(/\d+/)[0])).toLocaleDateString("en-US")),
    EmployeeID: ko.observable(parsedJSON.EmployeeID),
    
    DeleteEmployee: function () {
        $.ajax({
            url: '/Employee/Delete',
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
                window.location.href = '/Employee/Index/';
            }
        });
    }
};