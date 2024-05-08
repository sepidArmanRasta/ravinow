
function DeleteBlog(id) {
    Swal.fire({
        title: "آیا از حذف مطلب مطمئن هستید؟",
        text: "مطلب بعد از حذف بازگشت پذیر نیست",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "حذف",
        cancelButtonText: "انصراف"
    }).then((result) => {
        if (result.isConfirmed) {

            fetch("/Blog/Delete/" + id, {
                method: 'DELETE'
            }).then(response => response.text())
                .then(result => {
                    var Response = JSON.parse(result);
                    if (Response.success) {

                        window.location.href = "/Blog";

                    } else {
                        window.Notify(Response.systemMessage ? Response.systemMessage : Response.messages, "error");
                    }
                })
                .catch(error => {
                    window.Notify(error, "error");
                });
        }
    });
}