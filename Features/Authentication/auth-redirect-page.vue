<template>
    <div>
    </div>
</template>

<script>
    import { setAccessToken } from 'features/authentication/auth'   

    export default
        {                       
            mounted() {
                let token = this.$route.query.token

                if (token == null) {
                    this.$router.push('/')
                }
                setAccessToken(token)
                this.$router.push('/')
            }
        }
</script>

<style>

</style>
