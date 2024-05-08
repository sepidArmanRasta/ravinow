
let selectedEditor;

function edit(key) {
    var text = $('#' + key).html();

    DecoupledEditor
        .create(document.querySelector('#' + key), {
            language: 'fa',
            ckfinder: { uploadUrl: '../Shared/UploadFileBase64B' }
        })
        .then(editor => {
            selectedEditor = editor
            const toolbarContainer = document.querySelector('#' + key + 'Toolbar');
            toolbarContainer.prepend(editor.ui.view.toolbar.element);
            editor.setData(text);

            var edit = document.querySelectorAll('[data-type="edit-btn"]');

            for (var i = 0; i < edit.length; i++) {
                edit[i].setAttribute("hidden", "hidden");
            }

            document.getElementById(key + "-btn").removeAttribute("hidden");
        })
        .catch(err => {
            console.error(err.stack);
        });

}

function save(key) {
    var text = $("#" + key).html();

    document.getElementById(key + "-btn").removeAttribute("hidden");

    selectedEditor.destroy()
        .then(() => {
            const editorDiv = document.querySelector('#' + key);
            if (editorDiv) {
                editorDiv.innerHTML = text;
                const toolbarContainer = document.querySelector('#' + key + 'Toolbar');
                if (toolbarContainer) {
                    toolbarContainer.innerHTML = "";
                }
            }

            var edit = document.querySelectorAll('[data-type="edit-btn"]');

            for (var i = 0; i < edit.length; i++) {
                edit[i].removeAttribute("hidden");
            }

            document.getElementById(key + "-btn").setAttribute("hidden", "hidden");

            var model = JSON.stringify({
                "Description": text,
                "Source": key,
            });

            fetch("/Content/InsertContent", {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: model
            }).then(response => response.text())
                .then(result => {
                    var response = JSON.parse(result);

                    if (response.success) {
                        window.Notify(response.systemMessage ? response.systemMessage : response.messages, "success");
                    }
                    else {
                        window.Notify(response.systemMessage ? response.systemMessage : response.messages, "error");
                    }

                })
                .catch(error => {
                    window.Notify(error, "error");
                });



        })
        .catch(err => {

        });
}