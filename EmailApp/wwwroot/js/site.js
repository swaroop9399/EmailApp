const emailForm = document.getElementById('email-form');
const showImg = document.getElementById('show-img');

emailForm.onsubmit = function (e) {
    e.preventDefault();
    var formData = new FormData(e.target);
    let toEmail = formData.get('to');
    let subject = formData.get('body');
    let attachment = formData.get('attachment');

    console.log(attachment)
    $.ajax({
        type: 'POST',
        url: '/Home/SendMail',
        contentType: false,
        processData: false,
        data: formData,

        success: function (responseObject) {
            console.log(responseObject)
            /*showImg.src = 'data:image/png;base64,' + responseObject;*/
        }
    });
}