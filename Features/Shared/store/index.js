import Vue from 'vue'
import Vuex from 'vuex'
import { isLoggedIn } from 'features/authentication/auth';
import { getRSlashPopularFeed, getUsersSubsFeed } from 'features/reddit/api'


Vue.use(Vuex)

// TYPES
const SAVE_POSTS = 'SAVE_POSTS'

// STATE
const state = {
    posts: []
}

// MUTATIONS
const mutations = {
    [SAVE_POSTS](state, obj) {        
        state.posts.push(...obj.data)
    }
}

// ACTIONS
const actions = ({
    loadPosts({ commit }, opts) {
        if (!isLoggedIn())
            getRSlashPopularFeed(opts.skip, opts.take)
                .then((result) => commit(SAVE_POSTS, result))
        else
            getUsersSubsFeed(opts.skip, opts.take)
                .then((result) => commit(SAVE_POSTS, result))
    }
})

export default new Vuex.Store({
    state,
    mutations,
    actions
});
