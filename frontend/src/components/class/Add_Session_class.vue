<template>
  <div class="add_exam">
    <h1>
      {{ this.$route.query.action == "edit" ? "Cập nhật" : "Thêm" }} Tiết học
    </h1>
    <form @submit.prevent="onsubmit" class="from_add_exam">
      <div class="group_add_exam">
        <div v-if="$store.state.class.class" class="price_add_exam">
          <label>Thuộc lớp học</label>
          <input
            style="opacity: 0.5"
            disabled
            :value="
              $store.getters['class/getbyid'](Number(this.$route.query.id))[0]
                .name
            "
            type="text"
          />
        </div>
        <div class="price_add_exam">
          <label>Tên Tiết học</label>
          <input v-model="data.shift" type="text" />
          <span class="err" v-if="err.shift">{{ err.shift }}</span>
        </div>
      </div>

      <div class="group_add_exam">
        <div class="price_add_exam">
          <label>Từ ngày</label>
          <input v-model="data.start_date" type="date" />
          <span class="err" v-if="err.start_date">{{ err.start_date }}</span>
        </div>
        <!-- dropdown lanquage  -->
        <div class="price_add_exam">
          <label>Đến ngày</label>
          <input v-model="data.end_date" type="date" />
          <span class="err" v-if="err.end_date">{{ err.end_date }}</span>
        </div>
        <!-- dropdown lanquage -->
      </div>

      <!-- //group price and ducation -->
      <div class="group_add_exam">
        <div class="price_add_exam">
          <label>Giờ bắt đầu</label>
          <input v-model="data.start_hour" type="time" />
          <span class="err" v-if="err.start_hour">{{ err.start_hour }}</span>
        </div>
        <!-- dropdown lanquage  -->
        <div class="price_add_exam">
          <label>Giờ kết thúc</label>
          <input v-model="data.end_hour" type="time" />
          <span class="err" v-if="err.end_hour">{{ err.end_hour }}</span>
        </div>
        <!-- dropdown lanquage -->
      </div>
      <!-- end grop price and ducation -->

      <!-- group area -->
      <!-- end group area -->
      <!-- button fished -->
      <div class="btn_fished">
        <button>
          {{ this.$route.query.action == "edit" ? "Cập nhật" : "Hoàn tất" }}
        </button>
      </div>
      <!-- end button fished -->
    </form>
  </div>
</template>
<script>
import { computed, reactive, ref } from "vue";
import store from "@/store";
import { useToast } from "vue-toastification";
export default {
  name: "HomeView",
  created() {
    store.state.lesson.data_post.class_id = Number(this.$route.query.id);
    const check = setInterval(() => {
      if (store.state.lesson.lesson != null) {
        clearInterval(check);
        if (this.$route.query.action == "edit-lesson") {
          const x = store.getters["lesson/getbyid"](
            Number(this.$route.query.idus)
          )[0];
          store.state.lesson.data_post = x;
          store.state.lesson.data_post.start_hour = convert_tets(x.start_hour);
          store.state.lesson.data_post.end_hour = convert_tets(x.end_hour);
        }
      }
    }, 200);
    const convert_tets = (value) => {
      var hour = value.split(":")[0];
      var miu = value.split(":")[1];
      if (value.split(":")[0].length == 1) {
        hour = "0" + value.split(":")[0];
      }
      if (value.split(":")[1].length == 1) {
        miu = "0" + value.split(":")[1];
      }
      return hour + ":" + miu;
    };
  },
  setup() {
    const toast = useToast();
    store.dispatch("class/get_all");
    store.dispatch("course/get_all");
    store.dispatch("branch/get_all");
    store.dispatch("lesson/get_all");
    const status_dropdown_course = ref(null);
    const status_dropdown_branch = ref(null);
    const branch = computed(() => {
      return store.state.branch.branch;
    });
    const course = computed(() => {
      return store.state.course.course;
    });
    const data_fetch = reactive({
      name: null,
      id: null,
    });
    const status_dropdown = ref(false);

    const err = computed(() => {
      return store.state.lesson.err;
    });

    const data = computed(() => {
      return store.state.lesson.data_post;
    });

    const data_parnet = computed(() => {
      return store.getters["class/getbyid"](id_class.value)[0];
    });

    const convert_time = (value) => {
      return new Date(value).getTime();
    };
    const onsubmit = () => {
      data.value.shift == null || data.value.shift == ""
        ? (err.value.shift = "Không bỏ trống tên tiết học")
        : (err.value.shift = null);

      data.value.start_date == null
        ? (err.value.start_date = "Không bỏ trống ngày bắt đầu")
        : convert_time(data.value.start_date) < new Date().getTime()
        ? (err.value.start_date = "Phải lớn hơn ngày hiện tại")
        : (err.value.start_date = null);

      data.value.end_date == null
        ? (err.value.end_date = "Không bỏ trống ngày kết thúc")
        : (err.value.end_date = null);

      if (data.value.end_date != null && data.value.start_date != null) {
        new Date(data.value.start_date) >= new Date(data.value.end_date)
          ? (err.value.end_date = "Ngày kết thúc không hợp lệ")
          : (err.value.end_date = null);
        if (
          convert_time(data_parnet.value.start_date) >=
          convert_time(data.value.start_date)
        ) {
          err.value.start_date = "Nằm ngoài vùng cho phép";
        } else {
          err.value.start_date = null;
        }

        if (
          convert_time(data_parnet.value.end_date) <=
          convert_time(data.value.end_date)
        ) {
          err.value.end_date = "Nằm ngoài vùng cho phép";
        }
      }

      data.value.start_hour == null
        ? (err.value.start_hour = "Không bỏ trống giờ bắt đầu")
        : (err.value.start_hour = null);
      data.value.end_hour == null
        ? (err.value.end_hour = "Không bỏ trống giờ kết thúc")
        : (err.value.end_hour = null);
      if (
        data.value.end_hour != null &&
        data.value.start_hour != null &&
        data.value.end_date != null &&
        data.value.start_date != null
      ) {
        if (
          convert_time(data.value.start_hour + " " + data.value.end_date) >=
          convert_time(data.value.end_hour + " " + data.value.end_date)
        ) {
          err.value.end_hour = "Giờ kết thúc không hợp lệ";
        } else {
          err.value.end_hour = null;
        }
        // có thể post
        if (
          store.state.lesson.data_post.id != null &&
          (err.value.end_hour == null) & (err.value.end_date == null)
        ) {
          store.dispatch("lesson/update");
          toast.success("Cập nhật thành công");
        }
        if (err.value.end_hour == null && err.value.end_date == null) {
          store.dispatch("lesson/create");
          toast.success("Tạo Thành công");
        } else {
          if (store.state.lesson.data_post.id != null) {
            toast.error("Cập nhật thất bại");
          } else {
            toast.error("Tạo Thất bại");
          }
        }
      } else {
        if (store.state.lesson.data_post.id != null) {
          toast.error("Cập nhật thất bại");
        } else {
          toast.error("Tạo Thất bại");
        }
      }
    };
    const id_class = computed(() => {
      return store.state.lesson.data_post.class_id;
    });
    return {
      err,
      data_fetch,
      data,
      status_dropdown,
      onsubmit,
      status_dropdown_course,
      status_dropdown_branch,
      branch,
      course,
      data_parnet,
      id_class,
    };
  },
};
</script>
<style scoped>
.err {
  position: absolute;
  top: calc(100% + 10px);
  font-size: 14px;
  color: red;
}
/* button fished */
.btn_fished > button {
  border-radius: 10px;
  background: rgb(187, 4, 4);
  display: flex;
  width: 172px;
  height: 44px;
  padding: 5px 11px;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 10px;
  color: white;
  border: none;
}
.btn_fished {
  padding: 30px;
  display: flex;
  justify-content: center;
  align-items: center;
}
/* and button fished */
/* common  */
input {
  color: var(--color_main) !important;
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  height: var(--size-field_height);
  border-radius: 10px;
  border: 1px solid var(--color_main);
}
/* text area */
.group_area_exam {
  align-items: start;
  display: flex;
  flex-direction: column;
}
/* text area end */
.group_check_exam,
.group_area_exam {
  text-align: start;
}
.group_check {
  padding: 20px 0;
}
/* check radio */
.radio {
  display: none;
}
.icon_check {
  position: absolute;
  top: 50%;
  left: 50%;
  display: none;
  transform: translate(-50%, -50%);
}
.radio:checked ~ .btn > .icon_check {
  display: block;
}
.btn {
  position: relative;
  width: 25px;
  display: block;
  height: 25px;
  border-radius: 5px;
  border: 1px solid var(--color_main);
}
.group_check {
  display: flex;
  gap: 30px;
}
.group_check > label {
  display: flex;
  gap: 14px;
  align-items: center;
}
/* end radio */
/* set input  */
label {
  margin-bottom: 9px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}
/* end set input */
.name_add_exam,
.language_add_exam,
.price_add_exam,
.ducation_add_exam {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
.from_add_exam {
  border-radius: 10px;
  background: white;
  padding-top: 50px;
  padding-left: 50px;
  padding-right: 50px;
}

/* group name and language */
.group_add_exam {
  display: grid;
  grid-template-columns: 330px 330px;
  gap: 20px;
  margin-bottom: 40px;
}
.group_add_exam > div {
  position: relative;
  display: flex !important;
}
.group_add_exam > div > input,
.group_add_exam > div > div {
  width: 100% !important;
}
/* end group name and language */
h1 {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

/* drop down  */
.dropdown_input > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_input > .text {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
.dropdown_menu_show {
  opacity: 1 !important;
  pointer-events: visible !important;
}
.dropdown {
  position: relative;
}

.dropdown_input {
  position: relative;
  height: var(--size-field_height);
  background: white;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-weight: 600;
  font-size: 16px;
}
.dropdown_menu {
  transition: 0.1s;
  margin-top: 3px;
  border: 1px solid var(--color_main);
  position: absolute;
  left: 0;
  right: 0;
  opacity: 0;
  pointer-events: none;
  z-index: 1;
}
.dropdown_item {
  padding: 10px;
  background: white;
}
.dropdown_item:hover {
  background: var(--color_main);
  color: white;
}
/* end drop down  */
</style>
