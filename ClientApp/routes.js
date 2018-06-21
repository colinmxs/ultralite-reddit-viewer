const HomePage = () => import('components/home-page')
const AuthRedirectPage = () => import('components/auth-redirect-page')

export const routes = [
    { path: '/', component: HomePage, display: 'Home', style: 'glyphicon glyphicon-home' },
    { path:'/auth-redirect', component: AuthRedirectPage }
]
