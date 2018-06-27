<template>
    <div>
        <h1>Home Page</h1>        
        <post v-for="item in posts" :data="item"></post>
    </div>
</template>

<script>
    import { isLoggedIn } from 'features/authentication/auth';
    import { getRSlashPopularFeed, getUsersSubsFeed } from 'features/reddit/api'
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
                this.loadPosts()
            },
            methods: {
                isLoggedIn() {
                    return isLoggedIn();
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
