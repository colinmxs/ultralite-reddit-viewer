import Vue from 'vue'
import axios from 'axios'
import router from './router'
import store from 'shared/store'
import { sync } from 'vuex-router-sync'
import App from './app-root'

Vue.prototype.$http = axios;

sync(store, router)

const app = new Vue({
    store,
    router,
    ...App
})

export {
    app,
    router,
    store
}
