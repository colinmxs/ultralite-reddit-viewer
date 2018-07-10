<template>
    <div v-if="data.hide != 1">
        <md-card>
            <md-card-header>
                <md-card-header-text>
                    <div v-on:click="openSelf" class="md-body-1">{{data.title}}</div>
                    <div class="md-caption">in r/{{data.subreddit}} by u/{{data.author}}</div>
                </md-card-header-text>
                <md-card-media v-if="isImgUrl(data.url)">
                    <img :src="data.thumbnail" v-on:click="toggleImage" />
                </md-card-media>
            </md-card-header>
            <!--<router-link :to="{ name: 'comments', params: { id: data.id }}">Comments</router-link>-->
        </md-card>
        <image-viewer v-if="imageExpanded === 1" :url="data.url"></image-viewer>
    </div>
</template>

<script>
    export default
        {
            name: 'post',
            data() {
                return {
                    imageExpanded: 0
                }
            },
            props: ['data'],            
            components: {
                'image-viewer': () => import('./img')
            },
            methods: {
                isImgUrl(string) {
                    return /\.(jpg|gif|png)$/.test(string)
                },
                isSelf() {
                    return this.data.is_self && this.data.selftext.length > 0;
                },
                openSelf() {
                    
                },
                openExternal(url) {
                    window.location = url
                },
                toggleImage() {
                    this.imageExpanded === 1 ?
                        this.imageExpanded = 0 :
                        this.imageExpanded = 1
                }
            }
        }
</script>

<style scoped>
    .md-card {
        margin: 4px;        
    }
    
</style>
