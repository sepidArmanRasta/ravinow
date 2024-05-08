blogCategory = Vue.createApp(
    {
        data() {

            return {

                data: null,

                insertBlogDto: {
                    Title: '',
                    Body: '',
                    Description: '',
                    Photo: '',
                    FileName: '',
                    PublishDate : '',
                    AuthorId: null,
                    CategoryId: '',
                    Tags: '',
                },

                insertBlogDtoMessages: {
                    Title: '',
                    Body: '',
                    Description: '',
                    Photo: '',
                    BlogsPublishStatus: 0,
                    AuthorId: '',
                    CategoryId: '',
                    Tags: '',
                    price: '',
                },

                insertBlogCategoryDto: {
                    Name: '',
                    Photo: '',
                    FileName: '',
                    Description: '',
                    ParentCategoryId: null,
                },

                insertBlogCategoryDtoMessages: {
                    Name: '',
                    Photo: '',
                    Description: '',
                },

                blogCategoriesItems: [],
                FormInProgress: false,
                bodyEditor: null,
            };

        },

        mounted() {
            this.blogCategoryLoad();
            this.FormInProgress = false;

            const dtp1Instance = new mds.MdsPersianDateTimePicker(document.getElementById('dateSearch'), {
                targetTextSelector: '#dateSearch',
                targetDateSelector: '#dateSearch',
            });

            DecoupledEditor
                .create(document.querySelector('#editor'), {
                    language: 'fa',
                    ckfinder: { uploadUrl: '../Shared/UploadFileBase64B' }
                })
                .then(editor => {
                    const toolbarContainer = document.querySelector('#EditorToolbar');
                    toolbarContainer.prepend(editor.ui.view.toolbar.element);
                    this.bodyEditor = editor;
                    //editor.setData(window.Report);
                })
                .catch(err => {
                    console.error(err.stack);
                });
        },

        methods: {
            blogCategoryLoad: function () {


                fetch("/BlogCategory/GetList", {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                }).then(response => response.text())
                    .then(result => {
                        var response = JSON.parse(result);

                        if (response.success) {
                            blogCategory.blogCategoriesItems = response.data;
                        }
                        else {
                            window.Notify(response.systemMessage ? response.systemMessage : response.messages, "error");
                        }

                    })
                    .catch(error => {
                        window.Notify(error, "error");
                    });

            },
            blogLoad: function () {
                $.ajax({
                    url: "/Blogs/GetList",
                    type: "POST",
                    dataType: "json",
                    success: function (response) {

                        if (response.isSuccussed) {
                            blogCategory.blogItems = response.data;
                        }
                    },
                    error: function () {
                        console.error("blogLoad failed.");
                    }
                })
            },

            OnInsertBlog: function () {
                var modelIsValid = this.InsertBlogValidation(0);

                this.insertBlogDto.Body = $('#editor').html();

                if (modelIsValid) {
                    fetch("/Blog/Insert", {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.insertBlogDto)
                    }).then(response => response.text())
                        .then(result => {
                            var response = JSON.parse(result);

                            if (response.success) {
                                window.location.href = "/Blog";
                            }
                            else {
                                window.Notify(response.systemMessage ? response.systemMessage : response.messages, "error");
                            }

                        })
                        .catch(error => {
                            window.Notify(error, "error");
                        });
                }
            },

            OnDateChange: function () {
                var selectedDateTime = $('#dateSearch').val();
                this.insertBlogDto.PublishDate = selectedDateTime;
            },

            OnSelectImage: function (event) {

                const selectedFile = event.target.files[0];

                if (selectedFile) {
                    const reader = new FileReader();

                    reader.onload = function (event) {
                        const base64Image = event.target.result;
                        blogs.SelectedImageBase64 = base64Image;
                    }

                    reader.readAsDataURL(selectedFile);
                }
            },

            OnImageSelectorClick: function () {
                document.getElementById("categoryCover").click();
            },

            OnImageSelectorClickForBlog: function () {
                document.getElementById("blogCover").click();
            },

            uploadImage: function () {
                const selectedFile = $("#categoryCover")[0].files[0];

                if (selectedFile) {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        const imageName = selectedFile.name;
                        const base64String = e.target.result;

                        blogCategory.insertBlogCategoryDto.Photo = base64String;
                        blogCategory.insertBlogCategoryDto.FileName = imageName;
                    };
                    reader.readAsDataURL(selectedFile);
                }
            },

            uploadImageForBlog: function () {
                const selectedFile = $("#blogCover")[0].files[0];

                if (selectedFile) {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        const imageName = selectedFile.name;
                        const base64String = e.target.result;

                        blogCategory.insertBlogDto.Photo = base64String;
                        blogCategory.insertBlogDto.FileName = imageName;
                    };
                    reader.readAsDataURL(selectedFile);
                }
            },

            OnInsertBlogCategory: function () {
                var modelIsValid = this.InsertBlogCategoryValidation(0);

                if (modelIsValid) {

                    fetch("/BlogCategory/Insert", {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.insertBlogCategoryDto)
                    }).then(response => response.text())
                        .then(result => {
                            var response = JSON.parse(result);

                            if (response.success) {

                                window.Notify(response.systemMessage ? response.systemMessage : response.messages, "success");
                                $('#insertBlogCategoryModal').modal('hide');
                            }
                            else {
                                window.Notify(response.systemMessage ? response.systemMessage : response.messages, "error");
                            }

                            blogCategory.blogCategoryLoad();
                        })
                        .catch(error => {
                            window.Notify(error, "error");
                        });

                }
            },

            InsertBlogCategoryValidation: function (num) {
                var isValid = true;

                if (!this.insertBlogCategoryDto.Name) {
                    if (num == 1)
                        this.insertBlogCategoryDtoMessages.Name = 'نام نباید خالی باشد';
                    isValid = false;
                } else {
                    this.insertBlogCategoryDtoMessages.Name = '';
                }

                if (!this.insertBlogCategoryDto.Description) {
                    if (num == 2)
                        this.insertBlogCategoryDtoMessages.Description = 'توضیحات نباید خالی باشد';
                    isValid = false;
                } else {
                    this.insertBlogCategoryDtoMessages.Description = '';
                }

                if (!this.insertBlogCategoryDto.Photo) {
                    if (num == 3)
                        this.insertBlogCategoryDtoMessages.Photo = 'لطفا کاور را انتخاب کنید';
                    isValid = false;
                } else {
                    this.insertBlogCategoryDtoMessages.Photo = '';
                }

                return isValid;
            },

            ResetBlogCategoryModal: function () {
                this.insertBlogCategoryDto.Name = '';
                this.insertBlogCategoryDto.Description = '';
                this.insertBlogCategoryDto.Photo = '';
                this.insertBlogCategoryDto.ParentCategoryId = 0;
            },

            InsertBlogValidation: function (num) {
                var isValid = true;

                if (!this.insertBlogDto.Title) {
                    if (num == 1)
                        this.insertBlogDtoMessages.Title = "عنوان مطلب نمی‌تواند خالی باشد";
                    isValid = false;
                } else {
                    this.insertBlogDtoMessages.Title = "";
                }

                //if (!this.insertBlogDto.Body) {
                //    if (num == 2)
                //        this.insertBlogDtoMessages.Body = "بدنه مطلب نمی‌تواند خالی باشد";
                //    isValid = false;
                //} else {
                //    this.insertBlogDtoMessages.Body = "";
                //}

                if (!this.insertBlogDto.Description) {
                    if (num == 3)
                        this.insertBlogDtoMessages.Description = "توضیحات مطلب نمی‌تواند خالی باشد";
                    isValid = false;
                } else {
                    this.insertBlogDtoMessages.Description = "";
                }

                if (!this.insertBlogDto.Photo) {
                    if (num == 4)
                        this.insertBlogDtoMessages.Photo = "کاور مطلب نمی‌تواند خالی باشد";
                    isValid = false;
                } else {
                    this.insertBlogDtoMessages.Photo = "";
                }

                if (!this.insertBlogDto.Tags) {
                    if (num == 5)
                        this.insertBlogDtoMessages.Tags = "تگ‌های مطلب نمی‌تواند خالی باشد";
                    isValid = false;
                } else {
                    this.insertBlogDtoMessages.Tags = "";
                }

                return isValid;
            },
        }
    }).mount('#insertBlogs');

$('#insertBlogCategoryModal').on('hidden.bs.modal', function () {
    blogCategory.ResetBlogCategoryModal();
});