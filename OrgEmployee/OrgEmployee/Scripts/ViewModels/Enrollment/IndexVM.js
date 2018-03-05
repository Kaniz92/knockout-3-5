
$(function () {
    ko.applyBindings(IndexVM);
    IndexVM.getEnrollments();
});

var IndexVM = {
    Enrollments: ko.observableArray([]),
    getEnrollments: function () {
        var self = this;
        $.ajax({
            type: "GET",
            url: '/Enrollment/GetAllEnrollments',
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                self.Enrollments(data); //Put the response in ObservableArray
            },
            error: function (err) {
                console.log(err);
                alert(err.status + " : " + err.statusText);
            }
        });
    },
    CreateNew: function () {
        window.location.href = '/Enrollment/Create';
    },
    Back: function () {
        window.location.href = '/Home';
    },
    formatDate: function (textValue) {
        var date = new Date(parseInt(textValue.match(/\d+/)[0])).toLocaleDateString("en-US");
        return date;
    },
    EditEnrollment: function (enrollmentID) {
        window.location.href = '/Enrollment/Edit/' + enrollmentID;
    },
    DeleteEnrollment: function (enrollmentID) {
        window.location.href = '/Enrollment/Delete/' + enrollmentID;
    },
    ViewEnrollment: function (enrollmentID) {
        window.location.href = '/Enrollment/Details/' + enrollmentID;
    }
};

