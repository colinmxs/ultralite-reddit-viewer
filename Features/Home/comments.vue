<template>
    <div>
        <md-card>
            <md-card-header>
                <md-card-header-text>
                    <div class="md-body-1">{{data.title}}</div>
                    <div class="md-caption">in r/{{data.subreddit}} by u/{{data.author}}</div>
                </md-card-header-text>
                <md-card-media v-if="isImgUrl(data.url)">
                    <img :src="data.thumbnail" v-on:click="toggleImage" />
                </md-card-media>
            </md-card-header>
        </md-card>
    </div>
</template>

<script>
    export default {
        props: ['data']
    }
</script>

<style>

</style>
