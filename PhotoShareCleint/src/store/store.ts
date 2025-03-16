// store.js
import { combineSlices, configureStore } from "@reduxjs/toolkit";
import userSlice from "./userSlice";
import albumSlice from "./albumSlice";
import photoSlice from "./photoSlice";

const store = configureStore({
    reducer: combineSlices(
        userSlice,
        albumSlice,
        photoSlice
    ),
});

export type StoreType = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
