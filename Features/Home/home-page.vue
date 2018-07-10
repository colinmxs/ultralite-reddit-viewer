<template>
    <div class="home-content">        
        <post v-for="item in posts" :data="item"></post>
        <div class="text-center">
            <md-progress-spinner class="text-center" md-mode="indeterminate" v-if="showSpinner === 1"></md-progress-spinner>
            <a v-on:click="loadPosts(skip+25, 25)" v-else>Load More Posts...</a>
        </div>
    </div>
</template>

<script>    
    import Post from './post'
    import { mapState } from 'vuex'

    export default
        {
            data() {
                return {
                    showSpinner: 0,
                    skip: '',
                    take: ''
                }
            },
            computed: {
                ...mapState([
                    'posts'
                ])
            },
            components: {
                Post
            },
            mounted() {
                this.loadPosts(0, 25)
            },
            methods: {                               
                loadPosts(skip, take)
                {
                    this.showSpinner = 1
                    this.skip = skip
                    this.take = take
                    this.$store.dispatch('loadPosts', {
                        skip: skip,
                        take: take
                    })
                        .then(() => {
                            this.showSpinner = 0                            
                        })
                }
            }            
        }
</script>

<style scoped>
    .text-center{
        text-align: center;
    }
</style>
