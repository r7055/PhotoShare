// features/photoSlice.js
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import Swal from "sweetalert2";

const url = 'http://localhost:3000/api/photos';

// Async thunk for fetching photos
export const fetchPhotos = createAsyncThunk('photos/fetch',
    async (albumId, thunkAPI) => {
        try {
            const response = await axios.get(`${url}?albumId=${albumId}`);
            return response.data;
        } catch (e) {
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);

// Async thunk for adding a photo
export const addPhoto = createAsyncThunk('photos/add',
    async (photoData, thunkAPI) => {
        try {
            const response = await axios.post(url, photoData);
            return response.data.photo;
        } catch (e) {
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);

const photoSlice = createSlice({
    name: 'photos',
    initialState: { list: [], loading: true },
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchPhotos.fulfilled, (state, action) => {
                state.list = action.payload;
                state.loading = false;
            })
            .addCase(fetchPhotos.rejected, () => {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Couldn't fetch photos",
                });
            })
            .addCase(addPhoto.fulfilled, (state, action) => {
                state.list.push(action.payload);
                Swal.fire({
                    title: "Photo Added",
                    text: "The photo was successfully added.",
                    icon: "success",
                });
            })
            .addCase(addPhoto.rejected, () => {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Photo wasn't successfully added",
                });
            });
    }
});

export default photoSlice.reducer;
