package com.example.data.data_source

import com.example.core.DataClients.myBookClient
import com.example.domain.data_source.DataSource

class DataSources {
    companion object {
        val bookService: DataSource = myBookClient.create(DataSource::class.java)
    }
}