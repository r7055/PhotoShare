import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import { Photo } from '../types/photo';

const url = 'http://localhost:5141/api/Photos';

// Async thunk for searching all photos
export const searchAllPhotos = createAsyncThunk('photos/searchAllPhotos',
    async ({ token, query }: { token: string; query: string }, thunkAPI) => {
        try {
            const response = await axios.get<Photo[]>(`${url}/search`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
                params: { query },
            });
            return response.data;
        } catch (e: any) {
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);

const photosSlice = createSlice({
    name: 'photos',
    initialState: {
        photos: [] as Photo[],
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
            .addCase(searchAllPhotos.fulfilled, (state, action) => {
                state.photos = action.payload;
                state.loading = false;
                state.msg = '';
            })
            .addCase(searchAllPhotos.rejected, (state, action) => {
                state.loading = false;
                state.msg = action.payload as string || "Failed to search all photos";
            })
            .addCase(searchAllPhotos.pending, (state) => {
                state.loading = true;
            });
    },
});

export const { clearMessage } = photosSlice.actions;
export default photosSlice.reducer;
