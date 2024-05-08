portfolio = Vue.createApp(
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

                insertportfolioDto: {
                    Name: '',
                    Photo: '',
                    FileName: '',
                    Description: '',
                    ParentCategoryId: null,
                },

                insertportfolioDtoMessages: {
                    Name: '',
                    Photo: '',
                    Description: '',
                },

                blogCategoriesItems: [],
                FormInProgress: false,
                bodyEditor: null,
            };

        },

        methods: {
            blogLoad: function () {
                $.ajax({
                    url: "/Blogs/GetList",
                    type: "POST",
                    dataType: "json",
                    success: function (response) {

                        if (response.isSuccussed) {
                            portfolio.blogItems = response.data;
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

                        portfolio.insertportfolioDto.Photo = base64String;
                        portfolio.insertportfolioDto.FileName = imageName;
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

                        portfolio.insertBlogDto.Photo = base64String;
                        portfolio.insertBlogDto.FileName = imageName;
                    };
                    reader.readAsDataURL(selectedFile);
                }
            },

            OnInsertportfolio: function () {
                var modelIsValid = this.InsertportfolioValidation(0);

                if (modelIsValid) {

                    fetch("/portfolio/Insert", {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.insertportfolioDto)
                    }).then(response => response.text())
                        .then(result => {
                            var response = JSON.parse(result);

                            if (response.success) {

                                window.Notify(response.systemMessage ? response.systemMessage : response.messages, "success");
                                $('#insertportfolioModal').modal('hide');
                            }
                            else {
                                window.Notify(response.systemMessage ? response.systemMessage : response.messages, "error");
                            }

                            portfolio.portfolioLoad();
                        })
                        .catch(error => {
                            window.Notify(error, "error");
                        });

                }
            },

            InsertportfolioValidation: function (num) {
                var isValid = true;

                if (!this.insertportfolioDto.Name) {
                    if (num == 1)
                        this.insertportfolioDtoMessages.Name = 'نام نباید خالی باشد';
                    isValid = false;
                } else {
                    this.insertportfolioDtoMessages.Name = '';
                }

                if (!this.insertportfolioDto.Description) {
                    if (num == 2)
                        this.insertportfolioDtoMessages.Description = 'توضیحات نباید خالی باشد';
                    isValid = false;
                } else {
                    this.insertportfolioDtoMessages.Description = '';
                }

                if (!this.insertportfolioDto.Photo) {
                    if (num == 3)
                        this.insertportfolioDtoMessages.Photo = 'لطفا کاور را انتخاب کنید';
                    isValid = false;
                } else {
                    this.insertportfolioDtoMessages.Photo = '';
                }

                return isValid;
            },

            ResetportfolioModal: function () {
                this.insertportfolioDto.Name = '';
                this.insertportfolioDto.Description = '';
                this.insertportfolioDto.Photo = '';
                this.insertportfolioDto.ParentCategoryId = 0;
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
    }).mount('#portfolio');

$('#insertportfolioModal').on('hidden.bs.modal', function () {
    portfolio.ResetportfolioModal();
});