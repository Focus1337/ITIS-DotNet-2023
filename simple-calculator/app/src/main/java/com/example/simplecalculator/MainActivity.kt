package com.example.simplecalculator

import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.constraintlayout.widget.Group

class MainActivity : AppCompatActivity() {
    private lateinit var operationTextView: TextView
    private lateinit var resultTextView: TextView

    private fun Group.setAllOnClickListener(listener: View.OnClickListener?) {
        referencedIds.forEach { id ->
            rootView.findViewById<View>(id).setOnClickListener(listener)
        }
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val context = this@MainActivity
        val calculator = Calculator(context)

        operationTextView = findViewById(R.id.operationTextView)
        resultTextView = findViewById(R.id.resultTextView)

        val equalButton: Button = findViewById(R.id.equalButton)
        val defaultButton: Button = findViewById(R.id.defaultButton)

        val buttonGroup: Group = findViewById(R.id.buttonGroup)

        var curButton: Button
        var arg1 = ""
        var arg2 = ""
        var curOperation = Operation.Unknown
        val numbers = listOf("0", "1", "2", "3", "4", "5", "6", "7", "8", "9")

        buttonGroup.setAllOnClickListener {
            curButton = it as Button
            when {
                it == defaultButton -> Toast.makeText(context, "DEFAULT BUTTON", Toast.LENGTH_SHORT)
                    .show()

                curButton.text in numbers -> {
                    if (curOperation == Operation.Unknown) arg1 += curButton.text
                    else arg2 += curButton.text
                }

                curButton.text in calculator.operators -> {
                    if (arg1.isEmpty()) {
                        Toast.makeText(context, "ARG1 is EMPTY", Toast.LENGTH_SHORT).show()
                        return@setAllOnClickListener
                    }
                    if (curOperation != Operation.Unknown) {
                        Toast.makeText(
                            context,
                            "OPERATOR ALREADY CHOSEN: ${calculator.getOperator(curOperation)}",
                            Toast.LENGTH_SHORT
                        ).show()
                        return@setAllOnClickListener
                    }
                    curOperation = calculator.getOperator(curButton.text.toString())
                }

                curButton == equalButton -> when {
                    arg1.isEmpty() -> {
                        Toast.makeText(context, "ARG1 is EMPTY", Toast.LENGTH_SHORT).show()
                        return@setAllOnClickListener
                    }
                    arg2.isEmpty() -> {
                        Toast.makeText(context, "ARG2 is EMPTY", Toast.LENGTH_SHORT).show()
                        return@setAllOnClickListener
                    }
                    curOperation == Operation.Unknown -> {
                        Toast.makeText(context, "OPERATOR ISN'T CHOSEN", Toast.LENGTH_SHORT).show()
                        return@setAllOnClickListener
                    }
                    else -> {
                        resultTextView.text =
                            calculator.calculate(arg1.toInt(), curOperation, arg2.toInt())
                    }
                }

                else -> Toast.makeText(context, "UNKNOWN OPERATION", Toast.LENGTH_SHORT).show()
            }

            operationTextView.text =
                "$arg1 ${
                    when (curOperation) {
                        Operation.Unknown -> ""
                        else -> calculator.getOperator(curOperation)
                    }
                } $arg2"

            if (curButton == equalButton) {
                arg1 = ""
                arg2 = ""
                curOperation = Operation.Unknown
            }
        }
    }
}