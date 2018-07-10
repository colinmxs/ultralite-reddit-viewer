<template>
    <md-bottom-bar md-sync-route md-type="shift">
        <md-bottom-bar-item to="/"  md-label="Home" md-icon="home"></md-bottom-bar-item>
        <md-bottom-bar-item to="settings" id="bottom-bar-item-pages" md-label="Pages" md-icon="pages"></md-bottom-bar-item>
        <md-bottom-bar-item to="posts" id="bottom-bar-item-posts" md-label="Posts" md-icon="settings"></md-bottom-bar-item>
        <md-bottom-bar-item to="favorite" id="bottom-bar-item-favorites" md-label="Favorites" md-icon="favorite"></md-bottom-bar-item>
    </md-bottom-bar>
</template>

<script>
    import { routes } from 'features/routes'
    import { isLoggedIn, login, logout } from 'features/authentication/auth';
    export default {
        data() {
            return {
                routes,
                collapsed: true
            }
        },
        methods: {
            handleLogin() {
                login();
            },
            handleLogout() {
                logout();
            },
            isLoggedIn() {
                return isLoggedIn();
            },
        }
    }
</script>

<style scoped></style>
