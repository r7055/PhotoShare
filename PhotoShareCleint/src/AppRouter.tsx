import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import AlbumManager from './components/AlbumManager';
import AppLayout from './components/appLayout';
import UploadPhoto from './components/UploadPhoto';
import About from './components/About';
import Auth from './components/Auth';
import SignIn from './components/SignIn';
import Signup from './components/SignUp';

const router = createBrowserRouter([
    {
        path: '/',
        element: <AppLayout />,
        children: [
            {path: 'auth', element: <Auth />,
                children:[
                    {path: 'signin', element: <SignIn />},
                    {path: 'signup', element: <Signup />},
                ]
            },
          
            { path: '', element: <About /> },
            { path: 'auth', element: <Auth /> },
            { path: 'upload', element: <UploadPhoto /> },
            { path: 'albums', element: <AlbumManager /> },
            // כאן תוכל להוסיף עוד ניתובים
        ],
    },
]);

const AppRouter = () => {
    return <RouterProvider router={router} />;
};

export default AppRouter;
