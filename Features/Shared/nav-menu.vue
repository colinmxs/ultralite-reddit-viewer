<template>
    <div class="main-nav">
        <div class="navbar navbar-inverse">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" v-on:click="!collapsed">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/">Ultralite Viewer for Reddit!</a>
            </div>
            <div class="clearfix"></div>
            <transition name="slide">
                <div class="navbar-collapse collapse in" v-show="!collapsed">
                    <ul class="nav navbar-nav">
                        <li v-for="route in routes">
                            <!-- TODO: highlight active link -->
                            <router-link v-if="route.display != null" :to="route.path">
                                <span :class="route.style"></span> {{ route.display }}
                            </router-link>
                        </li>
                        <li>
                            <button class="btn btn-danger log" @click="handleLogout()">Log out </button>
                            <button class="btn btn-info log" @click="handleLogin()">Log In</button>
                        </li>
                    </ul>
                </div>
            </transition>
        </div>
    </div>
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

<style scoped>
    .slide-enter-active, .slide-leave-active {
        transition: max-height .35s
    }

    .slide-enter, .slide-leave-to {
        max-height: 0px;
    }

    .slide-enter-to, .slide-leave {
        max-height: 20em;
    }   

    .log {
        margin: 5px 10px 0 0;
    }
</style>
