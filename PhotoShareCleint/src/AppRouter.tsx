import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import AppLayout from './components/appLayout';
import UploadPhoto from './components/UploadPhoto';
import Auth from './components/Auth';
import SignIn from './components/SignIn';
import Signup from './components/SignUp';
import About from './components/About';
import AlbumList from './components/AlbumList';

const router = createBrowserRouter([
    {
        path: '/',
        element: <AppLayout />,
        children: [
            { path: '/', element: <About /> },
            { path: 'auth', element: <Auth /> },
            { path: 'signin', element: <SignIn /> },
            { path: 'signup', element: <Signup /> },
            { path: 'albumList', element: <AlbumList /> },
            { path: 'uploadPhoto', element: <UploadPhoto /> },
            // כאן תוכל להוסיף עוד ניתובים
        ],
    },
]);

const AppRouter = () => {
    return <RouterProvider router={router} />;
};

export default AppRouter;
