blogCategory = Vue.createApp(
    {
        data() {

            return {

                paginationRequestDto: {
                    Page: 1,
                    PageSize: 20,
                    SortType: 1,
                    Sort: 2,
                    Search: {
                        StringSearch: '',
                        InsertTimeFrom: '',
                        InsertTimeTo: '',
                        UpdateTimeFrom: '',
                        UpdateTimeTo: '',
                        CourseStatus: 0
                    }
                },
                data: null

            };

        },

        mounted() {
            this.LoadData();
        },

        methods: {

            LoadData: function () {

                fetch("/blogCategory/GetList", {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.paginationRequestDto)
                }).then(response => response.text())
                    .then(result => {
                        var Response = JSON.parse(result);
                        if (Response.success) {

                            blogCategory.data = Response.data;

                        } else {
                            window.Notify(Response.systemMessage ? Response.systemMessage : Response.messages, "error");
                        }
                    })
                    .catch(error => {
                        window.Notify(error, "error");
                    });
            },
        }
    }).mount('#blogCategory');