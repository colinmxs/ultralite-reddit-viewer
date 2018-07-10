import { BearerHTTP, HTTP } from 'shared/axios-base'
export { getRSlashPopularFeed, getUsersSubsFeed };

async function getRSlashPopularFeed(skip, take) {
    const url = '/api/reddit/popular?skip=' + skip + '&take=' + take;
    var response = await HTTP.get(url);
    return response.data;
}

function getUsersSubsFeed(skip, take) {
    const url = '/api/reddit/me?skip=' + skip + '&take=' + take;
    let http = BearerHTTP()
    return http.get(url).then(response => response.data);
}
