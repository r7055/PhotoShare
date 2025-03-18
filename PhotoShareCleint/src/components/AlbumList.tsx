import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import AlbumSearch from './AlbumSearch'; 
import { fetchAlbums } from '../store/albumSlice';
import { AppDispatch } from '../store/store';
import { Album } from '../types/album';

const AlbumList = () => {
    const dispatch = useDispatch<AppDispatch>();
    const { albums, loading, msg } = useSelector((state: { albums: { albums: Album[], loading: boolean, msg: string } }) => state.albums);
    const token = sessionStorage.getItem('token'); // Retrieve the token

    useEffect(() => {
        if (token) {
            dispatch(fetchAlbums(token)); // Fetch albums with token
        }
    }, [dispatch, token]);

    if (loading) return <div>Loading...</div>;
    if (msg) return <div>{msg}</div>;

    return (
        <div>
            {/* {albums.map(album => (
                <div key={album.id}>
                    <h3>{album.title}</h3>
                    <p>{album.description}</p>
                    <AlbumSearch albumId={Number(album.id)} />
                </div>
            ))} */}
            AlbumList
        </div>
    );
};

export default AlbumList;


// import React, { useEffect } from 'react';
// import { useDispatch, useSelector } from 'react-redux';
// import { fetchAlbums } from './slices/albumsSlice';
// import AlbumSearch from './AlbumSearch'; // Import the AlbumSearch component

// const AlbumList = () => {
//     const dispatch = useDispatch();
//     const { albums, loading, msg } = useSelector(state => state.albums);
//     const token = sessionStorage.getItem('token'); // Retrieve the token

//     useEffect(() => {
//         if (token) {
//             dispatch(fetchAlbums(token)); // Fetch albums with token
//         }
//     }, [dispatch, token]);

//     if (loading) return <div>Loading...</div>;
//     if (msg) return <div>{msg}</div>;

//     return (
//         <div>
//             {albums.map(album => (
//                 <div key={album.id}>
//                     <h3>{album.title}</h3>
//                     <p>{album.description}</p>
//                     <AlbumSearch albumId={album.id} /> {/* Add AlbumSearch for each album */}
//                 </div>
//             ))}
//         </div>
//     );
// };

// export default AlbumList;
