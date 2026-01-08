import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { getAllDebtors } from '../../services/debtors/debtors'; // adjust the path as needed

// Async thunk
export const fetchDebtors = createAsyncThunk(
  'debtors/fetchDebtors',
  async (_, { rejectWithValue }) => {
    try {
      const data = await getAllDebtors();
      return data;
    } catch (error) {
      return rejectWithValue(error.message);
    }
  }
);

// Slice
const debtorsSlice = createSlice({
  name: 'debtors',
  initialState: {
    debtors: [],
    status: 'idle', // 'idle' | 'loading' | 'succeeded' | 'failed'
    error: null
  },
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchDebtors.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(fetchDebtors.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.debtors = action.payload;
      })
      .addCase(fetchDebtors.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.payload;
      });
  }
});

export default debtorsSlice.reducer;
