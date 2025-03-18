import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import { Album } from '../types/album';

const url = 'http://localhost:5141/api/Albums';

// Async thunk for fetching albums
export const fetchAlbums = createAsyncThunk('albums/fetchAlbums',
    async (token: string, thunkAPI) => {
        try {
            const response = await axios.get<Album[]>(`${url}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            return response.data;
        } catch (e: any) {
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);

// Async thunk for searching photos in a specific album
export const searchPhotosInAlbum = createAsyncThunk('albums/searchPhotosInAlbum',
    async ({ token, albumId, query }: { token: string; albumId: string; query: string }, thunkAPI) => {
        try {
            const response = await axios.get(`${url}/${albumId}/photos/search`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
                params: { query },
            });
            return response.data; // Assuming the response contains the photos
        } catch (e: any) {
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);

const albumsSlice = createSlice({
    name: 'albums',
    initialState: {
        albums: [] as Album[],
        loading: false,
        msg: '',
    },
    reducers: {
        clearMessage: (state) => {
            state.msg = '';
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchAlbums.fulfilled, (state, action) => {
                state.albums = action.payload;
                state.loading = false;
                state.msg = '';
            })
            .addCase(fetchAlbums.rejected, (state, action) => {
                state.loading = false;
                state.msg = action.payload as string || "Failed to fetch albums";
            })
            .addCase(fetchAlbums.pending, (state) => {
                state.loading = true;
            })
            .addCase(searchPhotosInAlbum.fulfilled, (state, action) => {
                // Handle the search results if needed
            })
            .addCase(searchPhotosInAlbum.rejected, (state, action) => {
                state.loading = false;
                state.msg = action.payload as string || "Failed to search photos in album";
            })
            .addCase(searchPhotosInAlbum.pending, (state) => {
                state.loading = true;
            });
    },
});

export const { clearMessage } = albumsSlice.actions;
export default albumsSlice.reducer;
