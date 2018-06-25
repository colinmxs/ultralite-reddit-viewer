import axios from 'axios';
import Router from 'vue-router';
import { invalidateToken, getToken } from './api';
const ACCESS_TOKEN_KEY = 'access_token';

var router = new Router({
    mode: 'history',
});

export function login() {
    window.location = "/authentication/getcode"
}

export function logout() {
    invalidateToken()
        .then(() => {
            clearAccessToken();
            router.go('/');
        });
}

export function fetchToken(code, state) {
    getToken(code, state).then((token) => token);
}

export function getAccessToken() {
    return localStorage.getItem(ACCESS_TOKEN_KEY);
}

export function setAccessToken(accessToken) {    
    localStorage.setItem(ACCESS_TOKEN_KEY, accessToken);
}

export function isLoggedIn() {
    const accessToken = getAccessToken();
    return accessToken != null;
}

function clearAccessToken() {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
}
