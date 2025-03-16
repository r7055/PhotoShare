// features/albumSlice.js
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import Swal from "sweetalert2";

const url = 'http://localhost:3000/api/albums';

// Async thunk for fetching albums
export const fetchAlbums = createAsyncThunk('albums/fetch',
    async (_, thunkAPI) => {
        try {
            const response = await axios.get(url);
            return response.data;
        } catch (e) {
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);

// Async thunk for adding an album
export const addAlbum = createAsyncThunk('albums/add',
    async (albumData, thunkAPI) => {
        try {
            const response = await axios.post(url, albumData);
            return response.data.album;
        } catch (e) {
            return thunkAPI.rejectWithValue(e.message);
        }
    }
);

const albumSlice = createSlice({
    name: 'albums',
    initialState: { list: [], loading: true },
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchAlbums.fulfilled, (state, action) => {
                state.list = action.payload;
                state.loading = false;
            })
            .addCase(fetchAlbums.rejected, () => {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Couldn't fetch albums",
                });
            })
            .addCase(addAlbum.fulfilled, (state, action) => {
                state.list.push(action.payload);
                Swal.fire({
                    title: "Album Added",
                    text: "The album was successfully added.",
                    icon: "success",
                });
            })
            .addCase(addAlbum.rejected, () => {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Album wasn't successfully added",
                });
            });
    }
});

export default albumSlice.reducer;
