package com.example.secretgenerator.viewmodel

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.data.usecase.TodoUseCase
import com.example.domain.dto.TodoData
import kotlinx.coroutines.launch

class HomeViewModel : ViewModel() {

    private val todoUseCase = TodoUseCase()
    private val todoDataMutableMap = mutableMapOf<Int, MutableLiveData<TodoData>>()

    fun getTodoData(id: Int): MutableLiveData<TodoData> {
        return if (todoDataMutableMap.containsKey(id)) todoDataMutableMap[id]!!
        else {
            val data = MutableLiveData<TodoData>()
            viewModelScope.launch {
                data.postValue(todoUseCase.getTodo(id))
                todoDataMutableMap[id] = data
            }
            data
        }
    }
}
