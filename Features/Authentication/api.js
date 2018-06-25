export { invalidateToken, getToken };
import { BearerHTTP, HTTP } from 'shared/axios-base'

function invalidateToken() {
    const url = '/authentication/revoketoken';
    return BearerHTTP.get(url).then(response => response.data);
}

function getToken(code, state) {
    const url = '/authentication/gettoken?code=' + code;
    return HTTP.get(url).then(response => response.data);
}
