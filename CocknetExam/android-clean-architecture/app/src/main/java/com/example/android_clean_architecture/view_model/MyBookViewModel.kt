package com.example.android_clean_architecture.view_model

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.data.data_source.DataSources
import com.example.data.use_case.BackUseCaseImpl
import com.example.domain.dto.BooksData
import com.example.domain.use_case.BackUseCase
import kotlinx.coroutines.launch
import kotlin.random.Random

class MyBookViewModel : ViewModel() {
    companion object {
        var consuming = false
    }

    private val useCase: BackUseCase = BackUseCaseImpl()
    val booksDataMutable = MutableLiveData<List<BooksData>>()
    val bookDetailsDataMutableMap = mutableMapOf<String, MutableLiveData<BooksData>>()

    fun getBooks(token: String) {
        viewModelScope.launch {
            val books = useCase.getBooks(token)
            booksDataMutable.postValue(books!!)
        }
    }

//    fun addBook(token: String) {
//        viewModelScope.launch {
//            val book = useCase.addBook(token)
//            booksDataMutable.postValue(listOf(book!!))
//        }
//    }
//
//    fun deleteBook(token: String, id: String) {
//        viewModelScope.launch {
//            useCase.deleteBook(token, id)
//        }
//    }

}