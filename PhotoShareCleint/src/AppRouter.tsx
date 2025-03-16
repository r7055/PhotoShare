import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import AlbumManager from './components/AlbumManager';
import AppLayout from './components/appLayout';
import UploadPhoto from './components/UploadPhoto';
import About from './components/About';
import Auth from './components/Auth';

const router = createBrowserRouter([
    {
        path: '/',
        element: <AppLayout />,
        children: [
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
