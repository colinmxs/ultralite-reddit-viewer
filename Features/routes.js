const HomePage = () => import('features/Home/home-page')
const AuthRedirectPage = () => import('features/authentication/auth-redirect-page')

export const routes = [
    { path: '/', component: HomePage, display: 'Home', style: 'glyphicon glyphicon-home' },
    { path:'/auth-redirect', component: AuthRedirectPage }
]
