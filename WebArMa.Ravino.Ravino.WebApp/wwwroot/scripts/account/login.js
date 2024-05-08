login = Vue.createApp(
    {
        data() {

            return {

                captcha: '',

                loginDto: {
                    PhoneNumber: '',
                    Password: '',
                    IsPersistent: false,
                    CaptchaId: '',
                    CaptchaValue: '',
                },

                registerDto: {
                    FirstName: '',
                    LastName: '',
                    Password: '',
                    RePassword: '',
                    PhoneNumber: '',
                    CaptchaId: '',
                    CaptchaValue: '',
                },
                id: '',
            };

        },

        mounted() {
            this.GenerateCaptcha();
        },

        methods: {
            Login: function () {

                this.loginDto.CaptchaId = this.id;

                fetch("/Account/Login", {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.loginDto)
                }).then(response => response.text())
                    .then(result => {
                        var Response = JSON.parse(result);
                        if (Response.success) {

                            const params = new Proxy(new URLSearchParams(window.location.search), {
                                get: (searchParams, prop) => searchParams.get(prop),
                            });
                            let callBackUrl = params.callBackUrl;
                            window.location.href = ((callBackUrl != null && callBackUrl != "/") ? callBackUrl : "/");

                        } else {
                            window.Notify(Response.systemMessage ? Response.systemMessage : Response.messages, "error");
                            this.GenerateCaptcha();
                        }
                    })
                    .catch(error => {
                        window.Notify(error, "error");
                    });
            },
            Register: function () {

                this.registerDto.CaptchaId = this.id;

                fetch("/Account/Register", {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.registerDto)
                }).then(response => response.text())
                    .then(result => {
                        var Response = JSON.parse(result);
                        if (Response.success) {

                            const urlParams = new URLSearchParams(window.location.search);
                            const callBackUrl = urlParams.get('callBackUrl');
                            window.location.href = "/" + (callBackUrl != null ? callBackUrl : "");

                        } else {
                            window.Notify(Response.systemMessage ? Response.systemMessage : Response.messages, "error");
                            this.GenerateCaptcha();
                        }
                    })
                    .catch(error => {
                        window.Notify(error, "error");
                    });
            },
            GenerateCaptcha: function () {
                fetch("/Account/GenerateCaptcha", {
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

                            login.captcha = `data:image/png;base64, ${Response.data.captcha}`;
                            login.id = Response.data.id;

                        } else {
                            window.Notify(Response.systemMessage ? Response.systemMessage : Response.messages, "error");
                        }
                    })
                    .catch(error => {
                        window.Notify(error, "error");
                    });
            }
        }
    }).mount('#login');