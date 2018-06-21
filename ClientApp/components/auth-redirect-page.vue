<template>
    <div>
        Hello, Auth Redirect Page!
    </div>
</template>

<script>
    export default
        {
            data() {
                return {
                    oauthCode: this.$route.query.code,
                    oauthState: this.$route.query.state
                }
            },
            computed: {
                oauthToken: {
                    get() {
                        return localStorage.getItem('access_token')
                    },
                    set(newVal) {
                        localStorage.setItem('access_token', newVal)
                    }
                }
            },
            created() {
                this.fetchToken()
            },
            methods: {
                checkForOAuthCode() {
                    
                    if (this.oauthToken == null && this.oauthCode != null) {
                        this.fetchToken()
                    }
                },
                fetchToken() {
                    this.$http
                        .get('/authentication/gettoken?code=' + this.oauthCode)
                        .then(response => this.oauthToken = response.data)
                }
            }
        }
</script>

<style>

</style>
