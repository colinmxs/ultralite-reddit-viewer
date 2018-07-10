import { BearerHTTP, HTTP } from 'shared/axios-base'
export { getRSlashPopularFeed, getUsersSubsFeed };

function getRSlashPopularFeed(skip, take) {
    const url = '/api/reddit/popular?skip=' + skip + '&take=' + take;
    return HTTP.get(url).then(response => response.data);
}

function getUsersSubsFeed(skip, take) {
    const url = '/api/reddit/me?skip=' + skip + '&take=' + take;
    let http = BearerHTTP()
    return http.get(url).then(response => response.data);
}
