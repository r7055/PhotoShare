import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import AlbumManager from './components/AlbumManager';
import AppLayout from './components/appLayout';
import UploadPhoto from './components/UploadPhoto';
import Auth from './components/Auth';
import SignIn from './components/SignIn';
import Signup from './components/SignUp';
import About from './components/About';

const router = createBrowserRouter([
    {
        path: '/',
        element: <AppLayout />,
        children: [
            { path: '/', element: <About /> },
            { path: 'auth', element: <Auth /> },
            { path: 'signin', element: <SignIn /> },
            { path: 'signup', element: <Signup /> },
            { path: 'uploadPhoto', element: <UploadPhoto /> },
            { path: 'albums', element: <AlbumManager /> },
            // כאן תוכל להוסיף עוד ניתובים
        ],
    },
]);

const AppRouter = () => {
    return <RouterProvider router={router} />;
};

export default AppRouter;
