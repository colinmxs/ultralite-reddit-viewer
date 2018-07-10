<template>
    <div>
        <md-app md-mode="reveal">
            <md-app-toolbar class="md-primary">
                <md-button class="md-icon-button" @click="menuVisible = !menuVisible">
                    <md-icon>menu</md-icon>
                </md-button>
                <span class="md-title">Lizard Salsa</span>
            </md-app-toolbar>

            <md-app-drawer :md-active.sync="menuVisible">
                <md-toolbar class="md-transparent" md-elevation="0">{{isLoggedIn() ? "Profiles" : "Sign in below to load your subbies"}}</md-toolbar>

                <md-list v-if="isLoggedIn()">
                    <md-list-item v-for="item in profiles">
                        <md-icon>person</md-icon>
                        <span class="md-list-item-text">{{item.name}}</span>
                        <md-icon>clear</md-icon>
                    </md-list-item>
                    <md-divider class="md-inset"></md-divider>
                    <md-list-item>
                        <md-icon>person_add</md-icon>
                        <span class="md-list-item-text">Add profile</span>
                    </md-list-item>
                </md-list>
                <md-list v-else>
                    <md-list-item v-on:click="login">
                        <md-icon>person_outline</md-icon>
                        <span class="md-list-item-text">Sign in</span>
                    </md-list-item>
                </md-list>
            </md-app-drawer>

            <md-app-content>
                <router-view></router-view>
            </md-app-content>
        </md-app>
    </div>
</template>

<script>
    import Vue from 'vue'
    import NavMenu from 'shared/nav-menu'
    import { isLoggedIn, login, logout } from 'features/authentication/auth'
    Vue.component('nav-menu', NavMenu)

    export default {
        data: () => ({
            menuVisible: false,
            profiles: []
        }),
        computed: {
            login() {
                return login;
            },
            isLoggedIn() {
                return isLoggedIn;
            }
        }
    }
</script>

<style scoped>
    .md-app {
        height: 100vh;
        border: 1px solid rgba(#000, .12);
    }
    
</style>
