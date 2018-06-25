import { BearerHTTP, HTTP } from 'shared/axios-base'
export { getRSlashPopularFeed, getUsersSubsFeed, getUser };

function getRSlashPopularFeed(pageSize) {
    const url = '/api/reddit/popular?pageSize=' + pageSize;
    return HTTP.get(url).then(response => response.data);
}

function getUsersSubsFeed(pageSize) {
    const url = '/api/reddit/me?pageSize=' + pageSize;
    let http = BearerHTTP()
    return http.get(url).then(response => response.data);
}

function getUser() {
    const url = '/api/reddit/me';
    let http = BearerHTTP()
    return http.get(url).then(response => response.data);
}
