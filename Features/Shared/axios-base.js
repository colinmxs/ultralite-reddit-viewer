import axios from 'axios';
import { getAccessToken } from 'features/authentication/auth'

export const HTTP = axios.create()

export const BearerHTTP = () => axios.create({    
    headers: {
        Authorization: 'Bearer ' + getAccessToken()
    }
})
