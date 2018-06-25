<template>
    <div>
        <h1>Home Page</h1>        
        <post v-for="item in posts">{{item}}</post>
        <pre>{{user}}</pre>
    </div>
</template>

<script>
    import { isLoggedIn } from 'features/authentication/auth';
    import { getRSlashPopularFeed, getUsersSubsFeed, getUser } from 'features/reddit/api'
    import Post from './post'

    export default
        {
            data() {
                return {
                    posts: '',
                    pageSize: 10,
                    user: {}
                }
            },
            components: {
                Post
            },
            mounted() {
                if (this.isLoggedIn())
                    this.loadUser()
            },
            methods: {
                isLoggedIn() {
                    return isLoggedIn();
                },
                loadUser() {
                    getUser()
                        .then((user) => this.user = user)
                },
                loadPosts()
                {
                    if (!this.isLoggedIn())
                        getRSlashPopularFeed(this.pageSize)
                            .then((posts) =>
                            {
                                this.posts = posts
                            })
                    else
                        getUsersSubsFeed(this.pageSize)
                            .then((posts) => {
                                this.posts = posts
                            })                    
                }
            }
        }
</script>

<style>
</style>
