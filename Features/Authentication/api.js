export { invalidateToken };
import { BearerHTTP, HTTP } from 'shared/axios-base'

function invalidateToken() {
    const url = '/authentication/revoketoken';
    return BearerHTTP.get(url).then(response => response.data);
}
