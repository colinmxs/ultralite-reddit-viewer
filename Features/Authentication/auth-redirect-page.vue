<template>    
</template>

<script>
    import { fetchToken, setAccessToken } from 'features/authentication/auth'   

    export default
        {                       
            mounted() {
                let oauthCode = this.$route.query.code
                let oauthState = this.$route.query.state

                if (oauthCode == null) {
                    this.$route.go('/')
                }
                
                fetchToken(oauthCode, oauthState)
                    .then((token) => {
                        setAccessToken(token)
                        this.$router.push('/')
                    },
                    () => {
                        this.$router.push('/')
                    })                
            }
        }
</script>

<style>

</style>
